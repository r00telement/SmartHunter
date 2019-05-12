using SmartHunter.Core.Helpers;
using SmartHunter.Game.Data;
using SmartHunter.Game.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SmartHunter.Core.Config;

namespace SmartHunter.Game.Helpers
{
    public static class MhwHelper
    {
        private class DataOffsetContainer {
            public MonsterOffsets Monster = new MonsterOffsets();
            public MonsterModelOffsets MonsterModel = new MonsterModelOffsets();
            public MonsterHealthComponentOffsets MonsterHealthComponent = new MonsterHealthComponentOffsets();
            public MonsterPartCollectionOffsets MonsterPartCollection = new MonsterPartCollectionOffsets();
            public MonsterPartOffsets MonsterPart = new MonsterPartOffsets();
            public MonsterRemovablePartCollectionOffsets MonsterRemovablePartCollection = new MonsterRemovablePartCollectionOffsets();
            public MonsterRemovablePartOffsets MonsterRemovablePart = new MonsterRemovablePartOffsets();
            public MonsterStatusEffectCollectionOffsets MonsterStatusEffectCollection = new MonsterStatusEffectCollectionOffsets();
            public MonsterStatusEffectOffsets MonsterStatusEffect = new MonsterStatusEffectOffsets();
            public PlayerNameCollectionOffsets PlayerNameCollection = new PlayerNameCollectionOffsets();
            public PlayerDamageCollectionOffsets PlayerDamageCollection = new PlayerDamageCollectionOffsets();
            public PlayerDamageOffsets PlayerDamage = new PlayerDamageOffsets();
        }

        private static ConfigContainer<DataOffsetContainer> _dataOffsets;
        private static DataOffsetContainer DataOffsets => (_dataOffsets ?? (_dataOffsets = new ConfigContainer<DataOffsetContainer>(ConfigHelper.Main.Values.OffsetsFileName))).Values;

        public static void UpdatePlayerWidget(Process process, ulong baseAddress, ulong equipmentAddress, ulong weaponAddress)
        {
            for (int index = 0; index < ConfigHelper.PlayerData.Values.StatusEffects.Length; ++index)
            {
                var statusEffectConfig = ConfigHelper.PlayerData.Values.StatusEffects[index];

                ulong sourceAddress = baseAddress;
                if (statusEffectConfig.Source == Config.StatusEffectConfig.MemorySource.Equipment)
                {
                    sourceAddress = equipmentAddress;
                }
                else if (statusEffectConfig.Source == Config.StatusEffectConfig.MemorySource.Weapon)
                {
                    sourceAddress = weaponAddress;
                }
                
                bool allConditionsPassed = true;
                if (statusEffectConfig.Conditions != null)
                {
                    foreach (var condition in statusEffectConfig.Conditions)
                    {
                        bool isOffsetChainValid = true;
                        List<long> offsets = new List<long>();
                        foreach (var offsetString in condition.Offsets)
                        {
                            if (long.TryParse(offsetString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var offset))
                            {
                                offsets.Add(offset);
                            }
                            else
                            {
                                isOffsetChainValid = false;
                                break;
                            }
                        }

                        if (!isOffsetChainValid)
                        {
                            allConditionsPassed = false;
                            break;
                        }

                        var conditionAddress = MemoryHelper.ReadMultiLevelPointer(false, process, sourceAddress + (ulong)offsets.First(), offsets.Skip(1).ToArray());

                        bool isPassed = false;
                        if (condition.ByteValue.HasValue)
                        {
                            var conditionValue = MemoryHelper.Read<byte>(process, conditionAddress);
                            isPassed = conditionValue == condition.ByteValue;
                        }
                        else if (condition.IntValue.HasValue)
                        {
                            var conditionValue = MemoryHelper.Read<int>(process, conditionAddress);
                            isPassed = conditionValue == condition.IntValue;
                        }
                        else if (condition.StringRegexValue != null)
                        {
                            var conditionValue = MemoryHelper.ReadString(process, conditionAddress, 64);
                            isPassed = new Regex(condition.StringRegexValue).IsMatch(conditionValue);
                        }

                        if (!isPassed)
                        {
                            allConditionsPassed = false;
                            break;
                        }
                    }
                }

                float? timer = null;
                if (allConditionsPassed && statusEffectConfig.TimerOffset != null)
                {
                    if (ulong.TryParse(statusEffectConfig.TimerOffset, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var timerOffset))
                    {
                        timer = MemoryHelper.Read<float>(process, sourceAddress + timerOffset);
                    }

                    if (timer <= 0)
                    {
                        timer = 0;
                        allConditionsPassed = false;
                    }
                }

                OverlayViewModel.Instance.PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(index, timer, allConditionsPassed);
            }
        }

        public static void UpdateTeamWidget(Process process, ulong playerDamageCollectionAddress, ulong playerNameCollectionAddress)
        {
            List<Player> updatedPlayers = new List<Player>();
            for (int playerIndex = 0; playerIndex < DataOffsets.PlayerDamageCollection.MaxPlayerCount; ++playerIndex)
            {
                var player = UpdateAndGetTeamPlayer(process, playerIndex, playerDamageCollectionAddress, playerNameCollectionAddress);
                if (player != null)
                {
                    updatedPlayers.Add(player);
                }
            }

            if (updatedPlayers.Any())
            {
                OverlayViewModel.Instance.TeamWidget.Context.UpdateFractions();
            }
            else if (OverlayViewModel.Instance.TeamWidget.Context.Players.Any())
            {
                OverlayViewModel.Instance.TeamWidget.Context.Players.Clear();
            }
        }

        private static Player UpdateAndGetTeamPlayer(Process process, int playerIndex, ulong playerDamageCollectionAddress, ulong playerNameCollectionAddress)
        {
            Player player = null;

            var playerNameOffset = (ulong)DataOffsets.PlayerNameCollection.PlayerNameLength * (ulong)playerIndex;
            string name = MemoryHelper.ReadString(process, playerNameCollectionAddress + DataOffsets.PlayerNameCollection.FirstPlayerName + playerNameOffset, (uint)DataOffsets.PlayerNameCollection.PlayerNameLength);
            ulong firstPlayerPtr = playerDamageCollectionAddress + DataOffsets.PlayerDamageCollection.FirstPlayerPtr;
            ulong currentPlayerPtr = firstPlayerPtr + ((ulong)playerIndex * DataOffsets.PlayerDamageCollection.NextPlayerPtr);
            ulong currentPlayerAddress = MemoryHelper.Read<ulong>(process, currentPlayerPtr);
            int damage = MemoryHelper.Read<int>(process, currentPlayerAddress + DataOffsets.PlayerDamage.Damage);

            if (!String.IsNullOrEmpty(name) || damage > 0)
            {
                player = OverlayViewModel.Instance.TeamWidget.Context.UpdateAndGetPlayer(playerIndex, name, damage);
            }

            return player;
        }

        public static void UpdateMonsterWidget(Process process, ulong lastMonsterAddress)
        {
            if (lastMonsterAddress < 0xffffff)
            {
                OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Clear();
                return;
            }

            List<ulong> monsterAddresses = new List<ulong>();

            ulong currentMonsterAddress = lastMonsterAddress;
            while (currentMonsterAddress != 0)
            {
                monsterAddresses.Insert(0, currentMonsterAddress);
                currentMonsterAddress = MemoryHelper.Read<ulong>(process, currentMonsterAddress + DataOffsets.Monster.PreviousMonsterPtr);
            }

            List<Monster> updatedMonsters = new List<Monster>();
            foreach (var monsterAddress in monsterAddresses)
            {
                var monster = UpdateAndGetMonster(process, monsterAddress);
                if (monster != null)
                {
                    updatedMonsters.Add(monster);
                }
            }

            // Clean out monsters that aren't in the linked list anymore
            var obsoleteMonsters = OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Except(updatedMonsters);
            foreach (var obsoleteMonster in obsoleteMonsters.Reverse())
            {
                OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Remove(obsoleteMonster);
            }
        }

        private static Monster UpdateAndGetMonster(Process process, ulong monsterAddress)
        {
            Monster monster = null;

            ulong modelPtr = MemoryHelper.Read<ulong>(process, monsterAddress + DataOffsets.Monster.ModelPtr);
            string id = MemoryHelper.ReadString(process, modelPtr + DataOffsets.MonsterModel.Id, (uint)DataOffsets.MonsterModel.IdLength);

            if (String.IsNullOrEmpty(id))
            {
                return monster;
            }

            id = id.Split('\\').Last();
            if (!Monster.IsIncluded(id))
            {
                return monster;
            }

            ulong healthComponentAddress = MemoryHelper.Read<ulong>(process, monsterAddress + DataOffsets.Monster.PartCollection + DataOffsets.MonsterPartCollection.HealthComponentPtr);
            float maxHealth = MemoryHelper.Read<float>(process, healthComponentAddress + DataOffsets.MonsterHealthComponent.MaxHealth);
            if (maxHealth <= 0)
            {
                return monster;
            }

            float currentHealth = MemoryHelper.Read<float>(process, healthComponentAddress + DataOffsets.MonsterHealthComponent.CurrentHealth);
            float sizeScale = MemoryHelper.Read<float>(process, monsterAddress + DataOffsets.Monster.SizeScale);

            monster = OverlayViewModel.Instance.MonsterWidget.Context.UpdateAndGetMonster(monsterAddress, id, maxHealth, currentHealth, sizeScale);

            UpdateMonsterParts(process, monster);
            UpdateMonsterRemovableParts(process, monster);
            UpdateMonsterStatusEffects(process, monster);

            return monster;
        }

        private static void UpdateMonsterParts(Process process, Monster monster)
        {
            if (monster.Parts.Any())
            {
                foreach (var part in monster.Parts)
                {
                    UpdateMonsterPart(process, monster, part.Address);
                }
            }
            else
            {
                ulong firstPartAddress = monster.Address + DataOffsets.Monster.PartCollection + DataOffsets.MonsterPartCollection.FirstPart;

                for (int index = 0; index < DataOffsets.MonsterPartCollection.MaxItemCount; ++index)
                {
                    ulong currentPartOffset = DataOffsets.MonsterPart.NextPart * (ulong)index;
                    ulong currentPartAddress = firstPartAddress + currentPartOffset;

                    float maxHealth = MemoryHelper.Read<float>(process, currentPartAddress + DataOffsets.MonsterPart.MaxHealth);

                    // Read until we reach an element that has a max health of 0, which is presumably the end of the collection
                    if (maxHealth > 0)
                    {
                        UpdateMonsterPart(process, monster, currentPartAddress);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void UpdateMonsterPart(Process process, Monster monster, ulong partAddress)
        {
            float maxHealth = MemoryHelper.Read<float>(process, partAddress + DataOffsets.MonsterPart.MaxHealth);
            float currentHealth = MemoryHelper.Read<float>(process, partAddress + DataOffsets.MonsterPart.CurrentHealth);
            int timesBrokenCount = MemoryHelper.Read<int>(process, partAddress + DataOffsets.MonsterPart.TimesBrokenCount);

            monster.UpdateAndGetPart(partAddress, false, maxHealth, currentHealth, timesBrokenCount);
        }

        private static void UpdateMonsterRemovableParts(Process process, Monster monster)
        {
            if (monster.RemovableParts.Any())
            {
                foreach (var removablePart in monster.RemovableParts)
                {
                    UpdateMonsterRemovablePart(process, monster, removablePart.Address);
                }
            }
            else
            {
                ulong removablePartAddress = monster.Address + DataOffsets.Monster.RemovablePartCollection + DataOffsets.MonsterRemovablePartCollection.FirstRemovablePart;
                for (int index = 0; index < DataOffsets.MonsterRemovablePartCollection.MaxItemCount; ++index)
                {
                    // Every 16 elements there seems to be a new removable part collection. When we reach this point,
                    // we advance past the first 64 bit field to get to the start of the next part again
                    ulong staticPtr = MemoryHelper.Read<ulong>(process, removablePartAddress);
                    if (staticPtr <= 10)
                    {
                        removablePartAddress += 8;
                    }

                    // This is rough/hacky but it removes seemingly valid parts that aren't actually "removable".
                    // TODO: Figure out why Paolumu, Barroth, Radobaan have these mysterious removable parts
                    int validity1 = MemoryHelper.Read<int>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.Validity1);
                    int validity2 = MemoryHelper.Read<int>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.Validity2);
                    int validity3 = MemoryHelper.Read<int>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.Validity3);

                    bool isValid1 = validity1 == 1;
                    bool isValid2 = validity3 == 0 || validity3 == 1;

                    bool isValid3 = true;
                    if (validity3 == 0 && validity2 != 1)
                    {
                        isValid3 = false;
                    }

                    if (isValid1 && isValid2 && isValid3)
                    {
                        float maxHealth = MemoryHelper.Read<float>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.MaxHealth);
                        if (maxHealth > 0)
                        {
                            UpdateMonsterRemovablePart(process, monster, removablePartAddress);
                        }
                        else
                        {
                            break;
                        }
                    }

                    removablePartAddress += DataOffsets.MonsterRemovablePart.NextRemovablePart;
                }
            }
        }

        private static void UpdateMonsterRemovablePart(Process process, Monster monster, ulong removablePartAddress)
        {
            float maxHealth = MemoryHelper.Read<float>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.MaxHealth);
            float currentHealth = MemoryHelper.Read<float>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.CurrentHealth);
            int timesBrokenCount = MemoryHelper.Read<int>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.TimesBrokenCount);

            monster.UpdateAndGetPart(removablePartAddress, true, maxHealth, currentHealth, timesBrokenCount);
        }

        private static void UpdateMonsterStatusEffects(Process process, Monster monster)
        {
            if (monster.StatusEffects.Any())
            {
                foreach (var statusEffect in monster.StatusEffects)
                {
                    UpdateStatusEffect(process, monster, statusEffect.Address);
                }
            }
            else
            {
                ulong statusEffectCollectionAddress = monster.Address + DataOffsets.Monster.StatusEffectCollection;

                for (int index = 0; index < DataOffsets.MonsterStatusEffectCollection.MaxItemCount; ++index)
                {
                    ulong statusEffectPtr = statusEffectCollectionAddress + ((ulong)index * DataOffsets.MonsterStatusEffectCollection.NextStatusEffectPtr);
                    ulong statusEffectAddress = MemoryHelper.Read<ulong>(process, statusEffectPtr);

                    float maxDuration = MemoryHelper.Read<float>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.MaxDuration);
                    float maxBuildup = MemoryHelper.Read<float>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.MaxBuildup);
                    if (maxDuration > 0 || maxBuildup > 0)
                    {
                        UpdateStatusEffect(process, monster, statusEffectAddress);
                    }
                }
            }
        }

        private static void UpdateStatusEffect(Process process, Monster monster, ulong statusEffectAddress)
        {
            int id = MemoryHelper.Read<int>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.Id);
            float maxDuration = MemoryHelper.Read<float>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.MaxDuration);
            float currentBuildup = MemoryHelper.Read<float>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.CurrentBuildup);
            float maxBuildup = MemoryHelper.Read<float>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.MaxBuildup);
            float currentDuration = MemoryHelper.Read<float>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.CurrentDuration);
            int timesActivatedCount = MemoryHelper.Read<int>(process, statusEffectAddress + DataOffsets.MonsterStatusEffect.TimesActivatedCount);

            var statusEffect = monster.UpdateAndGetStatusEffect(statusEffectAddress, id, maxBuildup, currentBuildup, maxDuration, currentDuration, timesActivatedCount);
        }
    }
}
