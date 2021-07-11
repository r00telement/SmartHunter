using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartHunter.Core;
using SmartHunter.Core.Helpers;
using SmartHunter.Game.Config;
using SmartHunter.Game.Data;
using SmartHunter.Game.Data.ViewModels;

namespace SmartHunter.Game.Helpers
{
    public static class MhwHelper
    {
        public static bool TryParseHex(string hexString, out long hexNumber)
        {
            if (hexString.StartsWith("-"))
            {
                bool res = long.TryParse(hexString.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hexNumber);
                hexNumber = (-1) * hexNumber;
                return res;
            }
            return long.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hexNumber);
        }

        public static ulong AddOffset(ulong address, long offset)
        {
            return (ulong)((long)address + offset);
        }

        // TODO: Wouldn't it be nice if all this were data driven?
        public static class DataOffsets
        {
            public static class Monster
            {
                // Doubly linked list
                public static readonly ulong MonsterStartOfStructOffset = 0x40;
                public static readonly ulong NextMonsterOffset = 0x18;
                public static readonly ulong MonsterHealthComponentOffset = 0x7670;
                public static readonly ulong PreviousMonsterOffset = 0x10;
                public static readonly ulong SizeScale = 0x188;
                public static readonly ulong ScaleModifier = 0x7730;
                public static readonly ulong PartCollection = 0x14528;
                public static readonly ulong RemovablePartCollection = PartCollection + 0x22A0 - 0xF0 - 0xF0 - 0xF0;
                public static readonly ulong StatusEffectCollection = 0x19900;
                public static readonly ulong MonsterStaminaOffset = 0x1C130; //0x1BE20(???) 0x1C0D8(old) 0x1C130(now)
                public static readonly ulong MonsterRageOffset = 0x1BE88; //0x1BE20(???) 0x1BE30(old) 0x1BE88(now)
            }

            public static class MonsterModel
            {
                public static readonly int IdLength = 32; // 64?
                public static readonly ulong IdOffset = 0x179;
            }

            public static class MonsterHealthComponent
            {
                public static readonly ulong MaxHealth = 0x60;
                public static readonly ulong CurrentHealth = 0x64;
            }

            public static class MonsterPartCollection
            {
                public static readonly int MaxItemCount = 16;
                public static readonly ulong FirstPart = 0x50;
            }

            public static class MonsterPart
            {
                public static readonly ulong MaxHealth = 0x0C;
                public static readonly ulong CurrentHealth = 0x10;
                public static readonly ulong TimesBrokenCount = 0x18;
                public static readonly ulong NextPart = 0x1F8;//0x3F0;
            }

            public static class MonsterRemovablePartCollection
            {
                public static readonly int MaxItemCount = 32;
                public static readonly ulong FirstRemovablePart = 0x08;
            }

            public static class MonsterRemovablePart
            {
                public static readonly ulong MaxHealth = 0x0C;
                public static readonly ulong CurrentHealth = 0x10;
                public static readonly ulong TimesBrokenCount = 0x18;
                public static readonly ulong NextRemovablePart = 0x78;
            }

            public static class MonsterStatusEffectCollection
            {
                public static int MaxItemCount = 20;
                public static ulong NextStatusEffectPtr = 0x08;
            }

            public static class MonsterStatusEffect
            {
                public static readonly ulong MaxDuration = 0x19C;
                public static readonly ulong CurrentBuildup = 0x1B8;
                public static readonly ulong MaxBuildup = 0x1C8;
                public static readonly ulong CurrentDuration = 0x1F8;
                public static readonly ulong TimesActivatedCount = 0x200;
            }

            public static class PlayerNameCollection
            {
                public static readonly int IDLength = 12 + 1; // +1 for null terminator
                public static readonly int PlayerNameLength = 32 + 1; // +1 for null terminator
                public static readonly ulong FirstPlayerName = 0x532ED;
                public static readonly ulong SessionID = FirstPlayerName + 0xF43;
                public static readonly ulong SessionHostPlayerName = SessionID + 0x3F;
                public static readonly ulong LobbyID = FirstPlayerName + 0x463;
                public static readonly ulong LobbyHostPlayerName = LobbyID + 0x29;

                public static readonly ulong NextLobbyHostName = 0x2F; // Is dis even right?
            }
            public static class PlayerDamageCollection
            {
                public static readonly int MaxPlayerCount = 4;
                public static readonly ulong FirstPlayerPtr = 0x48;
                public static readonly ulong NextPlayerPtr = 0x58;
            }

            public static class PlayerDamage
            {
                public static readonly ulong Damage = 0x48;
                public static readonly int MaxOnScreenDamages = 14;
            }
        }

        private static int lastNetworkOperationTime = 0;
        private static bool networkOperationDone = true;
        private static int lastNetworkOperationTimeD = 0;
        private static bool networkOperationDoneD = true;
        private static int[,] expeditionDamageChecker = new int[DataOffsets.PlayerDamage.MaxOnScreenDamages, 2];

        public static void UpdateCurrentGame(Process process, ulong playerNameCollectionAddress, ulong currentPlayerNameAddress, ulong currentWeaponAddress, ulong lobbyStatusAddress)
        {
            string currentSessionID = MemoryHelper.ReadString(process, playerNameCollectionAddress + DataOffsets.PlayerNameCollection.SessionID, (uint)DataOffsets.PlayerNameCollection.IDLength);
            string currentSessionPlayerName = "";
            if (currentSessionID.Length > 0)
            {
                currentSessionPlayerName = MemoryHelper.ReadString(process, playerNameCollectionAddress + DataOffsets.PlayerNameCollection.SessionHostPlayerName, (uint)DataOffsets.PlayerNameCollection.PlayerNameLength);
            }

            string currentPlayerName = MemoryHelper.ReadString(process, currentPlayerNameAddress, (uint)DataOffsets.PlayerNameCollection.PlayerNameLength);

            string currentLobbyID = "";
            string currentLobbyPlayerName = "";
            bool isPlayerInMission = MemoryHelper.Read<uint>(process, lobbyStatusAddress + 0x54) != 0x0;
            bool isPlayerInExpedition = MemoryHelper.Read<uint>(process, lobbyStatusAddress + 0x38) != 0x1;
            if (isPlayerInMission || isPlayerInExpedition)
            {
                if (currentSessionID.Length > 0)
                {
                    currentLobbyID = MemoryHelper.ReadString(process, playerNameCollectionAddress + DataOffsets.PlayerNameCollection.LobbyID, (uint)DataOffsets.PlayerNameCollection.IDLength);
                    if (currentLobbyID.Length > 0)
                    {
                        for (int index = 0; index < 4; index++)
                        {
                            ulong PlayerNameOffset = DataOffsets.PlayerNameCollection.NextLobbyHostName * (ulong)index;
                            currentLobbyPlayerName = MemoryHelper.ReadString(process, playerNameCollectionAddress + DataOffsets.PlayerNameCollection.LobbyHostPlayerName + PlayerNameOffset, (uint)DataOffsets.PlayerNameCollection.PlayerNameLength);
                            if (currentLobbyPlayerName.Length > 0)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    currentLobbyID = "Not Online";
                    currentLobbyPlayerName = currentPlayerName;
                }
            }

            string currentEquippedWeaponString = MemoryHelper.ReadString(process, currentWeaponAddress, 0x4F);
            OverlayViewModel.Instance.DebugWidget.Context.UpdateCurrentGame(currentPlayerName, currentEquippedWeaponString, currentSessionID, currentSessionPlayerName, currentLobbyID, currentLobbyPlayerName, !isPlayerInMission && isPlayerInExpedition);
        }

        public static void UpdatePlayerWidget(Process process, ulong baseAddress, ulong equipmentAddress, ulong weaponAddress)
        {
            for (int index = 0; index < ConfigHelper.PlayerData.Values.StatusEffects.Length; ++index)
            {
                var statusEffectConfig = ConfigHelper.PlayerData.Values.StatusEffects[index];

                ulong sourceAddress = baseAddress;
                if (statusEffectConfig.Source == (uint)StatusEffectConfig.MemorySource.Equipment)
                {
                    sourceAddress = equipmentAddress;
                }
                else if (statusEffectConfig.Source != (uint)StatusEffectConfig.MemorySource.Base)
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
                            if (TryParseHex(offsetString, out var offset))
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
                if (statusEffectConfig.Source != (uint)StatusEffectConfig.MemorySource.Base && statusEffectConfig.Source != (uint)StatusEffectConfig.MemorySource.Equipment)
                {
                    if (!OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsValid)
                    {
                        continue;
                    }
                    if (OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.CurrentEquippedWeaponType() != (WeaponType)statusEffectConfig.Source)
                    {
                        allConditionsPassed = false;
                    }
                }
                float? timer = null;
                if (allConditionsPassed && statusEffectConfig.TimerOffset != null)
                {
                    if (TryParseHex(statusEffectConfig.TimerOffset, out var timerOffset))
                    {
                        timer = MemoryHelper.Read<float>(process, (ulong)((long)sourceAddress + timerOffset));
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
                OverlayViewModel.Instance.TeamWidget.Context.ClearPlayers();
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

        public static void UpdateDamageOnScreen(Process process, ulong damageOnScreenPtr)
        {
            var p = OverlayViewModel.Instance.TeamWidget.Context.Players.Where(p => p.Name.Equals(OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.CurrentPlayerName));
            if (p.Any())
            {
                var currentPlayer = p.First();
                ulong startOfList = damageOnScreenPtr + 0x2900;
                ulong currentItem = startOfList;
                for (int i = 0; i < DataOffsets.PlayerDamage.MaxOnScreenDamages; i++)
                {
                    int id1 = MemoryHelper.Read<int>(process, currentItem + 0x20);
                    int id2 = MemoryHelper.Read<int>(process, currentItem + 0x24);

                    if (expeditionDamageChecker[i, 0] != id1 && expeditionDamageChecker[i, 1] != id2)
                    {
                        expeditionDamageChecker[i, 0] = id1;
                        expeditionDamageChecker[i, 1] = id2;

                        int value = MemoryHelper.Read<int>(process, currentItem + 0x34);

                        currentPlayer.Damage += value;
                    }
                    currentItem += 0x90;
                }
                if (!OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsPlayerAlone() && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.playersCheckDone && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone && networkOperationDoneD && DateTime.Now.Second != lastNetworkOperationTimeD)
                {
                    networkOperationDoneD = false;
                    ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.DAMAGE, OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.key, OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.CurrentPlayerName, OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsCurrentPlayerLobbyHost(), currentPlayer.Damage, null, (result, ping) =>
                    {
                        if (result != null && result["status"].ToString().Equals("error"))
                        {
                            if (result["result"].ToString().Equals("0"))
                            {
                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone = false;
                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone = false;
                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.playersCheckDone = false;
                            }
                            else if (result["result"].ToString().Equals("false"))
                            {
                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.playersCheckDone = false;
                            }
                            else if (result["result"].ToString().Equals("v"))
                            {
                                ServerManager.Instance.IsServerOline = -1;
                                Core.Log.WriteLine("A new version is available, please update if you want to use the server!");
                            }
                            else if (result["result"].ToString().Equals("dev"))
                            {
                                ServerManager.Instance.IsServerOline = -1;
                                Core.Log.WriteLine("The server is under maintenance!");
                            }
                        }
                        else if (result != null && result["status"].ToString().Equals("ok"))
                        {
                            if (!result["result"].ToString().Equals(""))
                            {
                                var damageData = JsonConvert.DeserializeObject<Dictionary<string, int>>(result["result"].ToString());
                                foreach (var id in damageData.Keys)
                                {
                                    if (!id.Equals(OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.CurrentPlayerName))
                                    {
                                        var p = OverlayViewModel.Instance.TeamWidget.Context.Players.Where(p => p.Name.Equals(id));
                                        if (p.Any())
                                        {
                                            p.First().Damage = damageData[id];
                                        }
                                    }
                                }
                            }
                        }
                        networkOperationDoneD = true;
                        lastNetworkOperationTimeD = DateTime.Now.Second;
                    }, (error) =>
                    {
                        networkOperationDoneD = true;
                        lastNetworkOperationTimeD = DateTime.Now.Second;
                    });
                }
            }
        }

        public static void UpdateMonsterWidget(Process process, ulong monsterBaseList, ulong mapBaseAddress)
        {
            bool flg = false;
            if (mapBaseAddress != 0x0)
            {
                bool isMonsterSelected = MemoryHelper.Read<ulong>(process, mapBaseAddress + 0x128) != 0x0 && MemoryHelper.Read<ulong>(process, mapBaseAddress + 0x130) != 0x0 && MemoryHelper.Read<ulong>(process, mapBaseAddress + 0x160) != 0x0;
                if (isMonsterSelected)
                {
                    ulong selectedMonsterAddress = MemoryHelper.Read<ulong>(process, mapBaseAddress + 0x148) - 0X40;
                    var selectedMonster = UpdateAndGetMonster(process, selectedMonsterAddress);
                    if (selectedMonster != null)
                    {
                        flg = true;
                        var toRemoveMonsters = OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Where(m => !m.Id.Equals(selectedMonster.Id));
                        foreach (var obsoleteMonster in toRemoveMonsters.Reverse())
                        {
                            OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Remove(obsoleteMonster);
                        }
                    }
                }
            }

            if (!flg)
            {
                if (monsterBaseList < 0xffffff)
                {
                    OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Clear();
                    return;
                }

                List<ulong> monsterAddresses = new List<ulong>();

                ulong firstMonster = MemoryHelper.Read<ulong>(process, monsterBaseList + DataOffsets.Monster.PreviousMonsterOffset);

                if (firstMonster == 0x0)
                {
                    firstMonster = monsterBaseList;
                }

                firstMonster += DataOffsets.Monster.MonsterStartOfStructOffset;

                ulong currentMonsterAddress = firstMonster;
                while (currentMonsterAddress != 0)
                {
                    monsterAddresses.Insert(0, currentMonsterAddress);
                    currentMonsterAddress = MemoryHelper.Read<ulong>(process, currentMonsterAddress + DataOffsets.Monster.NextMonsterOffset);
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

            if (ConfigHelper.Main.Values.Overlay.MonsterWidget.UseNetworkServer && ServerManager.Instance.IsServerOline == 1 && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsValid && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsPlayerOnline())
            {
                if (OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsCurrentPlayerLobbyHost())
                {
                    if (!OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsPlayerAlone())
                    {
                        if (OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone)
                        {
                            if (networkOperationDone && DateTime.Now.Second != lastNetworkOperationTime)
                            {
                                Dictionary<string, Dictionary<string, Dictionary<string, int[]>>> data = new Dictionary<string, Dictionary<string, Dictionary<string, int[]>>>();
                                foreach (var monster in OverlayViewModel.Instance.MonsterWidget.Context.Monsters)
                                {
                                    if (OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsValid && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsPlayerOnline() && !OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsPlayerAlone())
                                    {
                                        if (OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone && networkOperationDone && DateTime.Now.Second != lastNetworkOperationTime)
                                        {
                                            Dictionary<string, Dictionary<string, int[]>> monsterData = new Dictionary<string, Dictionary<string, int[]>>();
                                            Dictionary<string, int[]> monsterPartsData = new Dictionary<string, int[]>();
                                            foreach (var part in monster.Parts)
                                            {
                                                int[] partValues = new int[4];
                                                partValues[0] = part.IsRemovable ? 1 : 0;
                                                partValues[1] = (int)part.Health.Max;
                                                partValues[2] = (int)part.Health.Current;
                                                partValues[3] = part.TimesBrokenCount;
                                                monsterPartsData.Add((monster.Parts.IndexOf(part) + 1).ToString(), partValues);
                                            }
                                            monsterData.Add("parts", monsterPartsData);

                                            Dictionary<string, int[]> monsterStatusesData = new Dictionary<string, int[]>();
                                            foreach (var status in monster.StatusEffects)
                                            {
                                                int[] statusValues = new int[5];
                                                statusValues[0] = (int)status.Buildup.Max;
                                                statusValues[1] = (int)status.Buildup.Current;
                                                statusValues[2] = (int)status.Duration.Max;
                                                statusValues[3] = (int)status.Duration.Current;
                                                statusValues[4] = (int)status.TimesActivatedCount;
                                                monsterStatusesData.Add(status.Index.ToString(), statusValues);
                                            }
                                            monsterData.Add("statuses", monsterStatusesData);
                                            data.Add(monster.Id, monsterData);
                                        }
                                    }
                                }
                                if (data.Keys.Count > 0)
                                {
                                    networkOperationDone = false;
                                    ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.PUSH, OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.key, null, true, 0, JsonConvert.SerializeObject(data), (result, ping) =>
                                    {
                                        if (result != null && result["status"].ToString().Equals("error"))
                                        {
                                            if (result["result"].ToString().Equals("false"))
                                            {
                                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone = false;
                                            }
                                            else if (result["result"].ToString().Equals("0"))
                                            {
                                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone = false;
                                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone = false;
                                            }
                                            else if (result["result"].ToString().Equals("v"))
                                            {
                                                ServerManager.Instance.IsServerOline = -1;
                                                Core.Log.WriteLine("A new version is available, please update if you want to use the server!");
                                            }
                                            else if (result["result"].ToString().Equals("dev"))
                                            {
                                                ServerManager.Instance.IsServerOline = -1;
                                                Core.Log.WriteLine("The server is under maintenance!");
                                            }
                                        }
                                        networkOperationDone = true;
                                        lastNetworkOperationTime = DateTime.Now.Second;
                                    }, (error) =>
                                    {
                                        networkOperationDone = true;
                                        lastNetworkOperationTime = DateTime.Now.Second;
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone && OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone)
                    {
                        if (networkOperationDone && DateTime.Now.Second != lastNetworkOperationTime)
                        {
                            networkOperationDone = false;
                            ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.PULL, OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.key, null, false, 0, null, (result, ping) =>
                            {
                                if (result != null)
                                {
                                    if (result["status"].ToString().Equals("ok"))
                                    {
                                        if (!result["result"].ToString().Equals(""))
                                        {
                                            var monstersData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, int[]>>>>(result["result"].ToString());//.ToObject<Dictionary<string, Dictionary<string, Dictionary<string, int[]>>>>();
                                            foreach (var id in monstersData.Keys)
                                            {
                                                var m = OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Where(m => m.Id.Equals(id));
                                                if (m.Any())
                                                {
                                                    var monster = m.First();
                                                    var monsterData = monstersData[monster.Id];
                                                    var monsterPartsData = monsterData["parts"];
                                                    var monsterStatusesData = monsterData["statuses"];

                                                    UpdateMonsterParts(monsterPartsData, monster);
                                                    UpdateMonsterStatusEffects(monsterStatusesData, monster);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (result["status"].ToString().Equals("error"))
                                        {
                                            if (result["result"].ToString().Equals("false"))
                                            {
                                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone = false;
                                            }
                                            else if (result["result"].ToString().Equals("0"))
                                            {
                                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone = false;
                                                OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone = false;
                                            }
                                            else if (result["result"].ToString().Equals("v"))
                                            {
                                                ServerManager.Instance.IsServerOline = -1;
                                                Core.Log.WriteLine("A new version is available, please update if you want to use the server!");
                                            }
                                            else if (result["result"].ToString().Equals("dev"))
                                            {
                                                ServerManager.Instance.IsServerOline = -1;
                                                Core.Log.WriteLine("The server is under maintenance!");
                                            }
                                        }
                                    }
                                }
                                networkOperationDone = true;
                                lastNetworkOperationTime = DateTime.Now.Second;
                            }, (error) =>
                            {
                                networkOperationDone = true;
                                lastNetworkOperationTime = DateTime.Now.Second;
                            });
                        }
                    }
                }
            }
        }

        private static Monster UpdateAndGetMonster(Process process, ulong monsterAddress)
        {
            Monster monster = null;

            ulong tmp = monsterAddress + DataOffsets.Monster.MonsterStartOfStructOffset + DataOffsets.Monster.MonsterHealthComponentOffset;
            ulong health_component = MemoryHelper.Read<ulong>(process, tmp);

            string id = MemoryHelper.ReadString(process, tmp + DataOffsets.MonsterModel.IdOffset, (uint)DataOffsets.MonsterModel.IdLength);
            float maxHealth = MemoryHelper.Read<float>(process, health_component + DataOffsets.MonsterHealthComponent.MaxHealth);

            if (String.IsNullOrEmpty(id))
            {
                return monster;
            }

            id = id.Split('\\').Last();
            if (!Monster.IsIncluded(id))
            {
                return monster;
            }

            if (maxHealth <= 0)
            {
                return monster;
            }

            float currentHealth = MemoryHelper.Read<float>(process, health_component + DataOffsets.MonsterHealthComponent.CurrentHealth);
            float sizeScale = MemoryHelper.Read<float>(process, monsterAddress + DataOffsets.Monster.MonsterStartOfStructOffset + DataOffsets.Monster.SizeScale);
            float scaleModifier = MemoryHelper.Read<float>(process, monsterAddress + DataOffsets.Monster.MonsterStartOfStructOffset + DataOffsets.Monster.ScaleModifier);
            if (scaleModifier <= 0 || scaleModifier >= 2)
            {
                scaleModifier = 1;
            }

            monster = OverlayViewModel.Instance.MonsterWidget.Context.UpdateAndGetMonster(monsterAddress, id, maxHealth, currentHealth, sizeScale, scaleModifier);

            if (ConfigHelper.MonsterData.Values.Monsters.ContainsKey(id) && ConfigHelper.MonsterData.Values.Monsters[id].Parts != null && ConfigHelper.MonsterData.Values.Monsters[id].Parts.Count() > 0)
            {
                if (OverlayViewModel.Instance.MonsterWidget.Context.AlwaysShowParts || (!OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsValid || OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsCurrentPlayerLobbyHost() || !OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.IsPlayerOnline()))
                {
                    UpdateMonsterParts(process, monster);
                    if (ConfigHelper.MonsterData.Values.Monsters[id].Parts.Where(p => p.IsRemovable).Count() > 0) // In case you are testing add "|| true"
                    {
                        UpdateMonsterRemovableParts(process, monster);
                    }
                    UpdateMonsterStatusEffects(process, monster);
                }
                else
                {
                    if (!OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.helloDone || !OverlayViewModel.Instance.DebugWidget.Context.CurrentGame.checkDone)
                    {
                        UpdateMonsterStatusEffects(process, monster);
                    }
                }
            }

            return monster;
        }

        private static void UpdateMonsterParts(Dictionary<string, int[]> parts, Monster monster)
        {
            foreach (KeyValuePair<string, int[]> entry in parts)
            {
                monster.UpdateAndGetPart(ulong.Parse(entry.Key), entry.Value[0] == 1, entry.Value[1], entry.Value[2], (int)entry.Value[3]);
            }
        }

        private static void UpdateMonsterStatusEffects(Dictionary<string, int[]> statuses, Monster monster)
        {
            foreach (KeyValuePair<string, int[]> entry in statuses)
            {
                monster.UpdateAndGetStatusEffect(ulong.Parse(entry.Key), int.Parse(entry.Key), entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[3], (int)entry.Value[4]);
            }
        }

        private static void UpdateMonsterParts(Process process, Monster monster)
        {
            var parts = monster.Parts.Where(part => !part.IsRemovable);
            if (parts.Any())
            {
                foreach (var part in parts)
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

                    if (maxHealth > 0)
                    {
                        UpdateMonsterPart(process, monster, currentPartAddress);
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
            var removableParts = monster.Parts.Where(part => part.IsRemovable);
            if (removableParts.Any())
            {
                foreach (var removablePart in removableParts)
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

                    uint maxRemovableParts = (uint)ConfigHelper.MonsterData.Values.Monsters[monster.Id].Parts.Where(p => p.IsRemovable).Count();
                    bool isLast = MemoryHelper.Read<uint>(process, removablePartAddress + 0x94) == 0x1;
                    //bool isValid = MemoryHelper.Read<byte>(process, removablePartAddress + 0x15) == 0;
                    bool isValid = MemoryHelper.Read<uint>(process, removablePartAddress + 0x6C) < maxRemovableParts;

                    if (isValid)
                    {
                        float maxHealth = MemoryHelper.Read<float>(process, removablePartAddress + DataOffsets.MonsterRemovablePart.MaxHealth);
                        if (maxHealth > 0)
                        {
                            UpdateMonsterRemovablePart(process, monster, removablePartAddress);
                            if (isLast || monster.Parts.Where(p => p.IsRemovable).Count() == maxRemovableParts)
                            {
                                break;
                            }
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
            int maxIndex = ConfigHelper.MonsterData.Values.StatusEffects.Where(s => s.GroupId.Equals("StatusEffect")).Count() - 1;
            var statuses = monster.StatusEffects;
            if (statuses != null && statuses.Where(s => s.GroupId.Equals("StatusEffect")).Any())
            {
                for (int i = 0; i < statuses.Count(); i++)
                {
                    MonsterStatusEffect status = statuses[i];
                    if (status == null)
                    {
                        continue;
                    }
                    float currentBuildup = 0;
                    float maxBuildup = MemoryHelper.Read<float>(process, status.Address + DataOffsets.MonsterStatusEffect.MaxBuildup);
                    if (maxBuildup > 0)
                    {
                        currentBuildup = MemoryHelper.Read<float>(process, status.Address + DataOffsets.MonsterStatusEffect.CurrentBuildup);
                    }
                    float currentDuration = 0;
                    float maxDuration = MemoryHelper.Read<float>(process, status.Address + DataOffsets.MonsterStatusEffect.MaxDuration);
                    if (maxDuration > 0)
                    {
                        currentDuration = MemoryHelper.Read<float>(process, status.Address + DataOffsets.MonsterStatusEffect.CurrentDuration);
                    }
                    int timesActivatedCount = MemoryHelper.Read<int>(process, status.Address + DataOffsets.MonsterStatusEffect.TimesActivatedCount);

                    if (maxBuildup > 0 || maxDuration > 0)
                    {
                        uint index = MemoryHelper.Read<uint>(process, status.Address + 0x198);
                        if (index <= maxIndex)
                        {
                            var statusEffectConfig = ConfigHelper.MonsterData.Values.StatusEffects[index];
                            monster.UpdateAndGetStatusEffect(status.Address, (int)index, maxBuildup > 0 ? maxBuildup : 1, !statusEffectConfig.InvertBuildup ? currentBuildup : maxBuildup - currentBuildup, maxDuration, !statusEffectConfig.InvertDuration ? currentDuration : maxDuration - currentDuration, timesActivatedCount);
                        }
                    }
                }
            }
            else
            {
                ulong baseStatus = MemoryHelper.Read<ulong>(process, monster.Address + DataOffsets.Monster.MonsterStartOfStructOffset + 0x78);
                baseStatus = MemoryHelper.Read<ulong>(process, baseStatus + 0x57A8);
                ulong nani = baseStatus;
                while (nani != 0)
                {
                    nani = MemoryHelper.Read<ulong>(process, nani + 0x10);
                    if (nani != 0)
                    {
                        baseStatus = nani;
                    }
                }
                ulong currentStatusPointer = baseStatus + 0x40;
                while (currentStatusPointer != 0x0)
                {
                    var currentMonsterInStatus = MemoryHelper.Read<ulong>(process, currentStatusPointer + 0x188);
                    if (currentMonsterInStatus == monster.Address + 0x40 && !monster.StatusEffects.Where(status => status.Address == currentStatusPointer).Any())
                    {
                        float currentBuildup = 0;
                        float maxBuildup = MemoryHelper.Read<float>(process, currentStatusPointer + DataOffsets.MonsterStatusEffect.MaxBuildup);
                        if (maxBuildup > 0)
                        {
                            currentBuildup = MemoryHelper.Read<float>(process, currentStatusPointer + DataOffsets.MonsterStatusEffect.CurrentBuildup);
                        }
                        float currentDuration = 0;
                        float maxDuration = MemoryHelper.Read<float>(process, currentStatusPointer + DataOffsets.MonsterStatusEffect.MaxDuration);
                        if (maxDuration > 0)
                        {
                            currentDuration = MemoryHelper.Read<float>(process, currentStatusPointer + DataOffsets.MonsterStatusEffect.CurrentDuration);
                        }
                        int timesActivatedCount = MemoryHelper.Read<int>(process, currentStatusPointer + DataOffsets.MonsterStatusEffect.TimesActivatedCount);

                        if (maxBuildup > 0 || maxDuration > 0)
                        {
                            uint index = MemoryHelper.Read<uint>(process, currentStatusPointer + 0x198);
                            if (index <= maxIndex && !((index == 14 || index == 15) && monster.isElder) && index != 0) // skip traps for elders
                            {
                                var statusEffectConfig = ConfigHelper.MonsterData.Values.StatusEffects[index];
                                monster.UpdateAndGetStatusEffect(currentStatusPointer, (int)index, maxBuildup > 0 ? maxBuildup : 1, !statusEffectConfig.InvertBuildup ? currentBuildup : maxBuildup - currentBuildup, maxDuration, !statusEffectConfig.InvertDuration ? currentDuration : maxDuration - currentDuration, timesActivatedCount);
                            }
                        }
                    }
                    currentStatusPointer = MemoryHelper.Read<ulong>(process, currentStatusPointer + 0x18);
                }
            }

            // Stamina

            ulong staminaAddress = monster.Address + DataOffsets.Monster.MonsterStaminaOffset;
            float maxStaminaBuildUp = MemoryHelper.Read<float>(process, staminaAddress + 0x4);
            float currentStaminaBuildUp = 0;
            if (maxStaminaBuildUp > 0)
            {
                currentStaminaBuildUp = MemoryHelper.Read<float>(process, staminaAddress);
            }
            float maxFatigueDuration = MemoryHelper.Read<float>(process, staminaAddress + 0xC);
            float currentFatigueDuration = 0;
            if (maxFatigueDuration > 0)
            {
                currentFatigueDuration = MemoryHelper.Read<float>(process, staminaAddress + 0x10);
            }
            int fatigueActivatedCount = MemoryHelper.Read<int>(process, staminaAddress + 0x14);
            MonsterStatusEffectConfig statusEffect = null;
            if (currentFatigueDuration > 0)
            {
                statusEffect = ConfigHelper.MonsterData.Values.StatusEffects.SingleOrDefault(s => s.GroupId.Equals("Fatigue"));
            }
            else
            {
                statusEffect = ConfigHelper.MonsterData.Values.StatusEffects.SingleOrDefault(s => s.GroupId.Equals("Stamina"));
            }

            if (maxStaminaBuildUp > 0 || currentFatigueDuration > 0)
            {
                monster.UpdateAndGetStatusEffect(staminaAddress, Array.IndexOf(ConfigHelper.MonsterData.Values.StatusEffects, statusEffect), maxStaminaBuildUp > 0 ? maxStaminaBuildUp : 1, !statusEffect.InvertBuildup ? currentStaminaBuildUp : maxStaminaBuildUp - currentStaminaBuildUp, maxFatigueDuration, !statusEffect.InvertDuration ? currentFatigueDuration : maxFatigueDuration - currentFatigueDuration, fatigueActivatedCount);
            }

            // Rage

            ulong rageAddress = monster.Address + DataOffsets.Monster.MonsterRageOffset;
            float maxRageBuildUp = MemoryHelper.Read<float>(process, rageAddress + 0x24);
            float currentRageBuildUp = 0;
            if (maxRageBuildUp > 0)
            {
                currentRageBuildUp = MemoryHelper.Read<float>(process, rageAddress);
            }
            float maxRageDuration = MemoryHelper.Read<float>(process, rageAddress + 0x10);
            float currentRageDuration = 0;
            if (maxRageDuration > 0)
            {
                currentRageDuration = MemoryHelper.Read<float>(process, rageAddress + 0xC);
            }
            int rageActivatedCount = MemoryHelper.Read<int>(process, rageAddress + 0x1C);
            var rageStatusEffect = ConfigHelper.MonsterData.Values.StatusEffects.SingleOrDefault(s => s.GroupId.Equals("Rage"));//[33]; // 33 is rage
            if (maxRageBuildUp > 0 || maxRageDuration > 0)
            {
                monster.UpdateAndGetStatusEffect(rageAddress, Array.IndexOf(ConfigHelper.MonsterData.Values.StatusEffects, rageStatusEffect), maxRageBuildUp > 0 ? maxRageBuildUp : 1, !rageStatusEffect.InvertBuildup ? currentRageBuildUp : maxRageBuildUp - currentRageBuildUp, maxRageDuration, !rageStatusEffect.InvertDuration ? currentRageDuration : maxRageDuration - currentRageDuration, rageActivatedCount);
            }
        }
    }
}
