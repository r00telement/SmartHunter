using SmartHunter.Core;

namespace SmartHunter.Game.Helpers
{
    public static class LocalizationHelper
    {
        public static readonly string MissingStringId = "LOC_MISSING";
        public static readonly string UnknownMonsterStringId = "LOC_MONSTER_UKNOWN";
        public static readonly string UnknownPartStringId = "LOC_PART_UKNOWN";
        public static readonly string UnknownRemovablePartStringId = "LOC_REMOVABLE_PART_UKNOWN";
        public static readonly string UnknownMonsterStatusEffectStringId = "LOC_STATUS_EFFECT_UKNOWN";
        public static readonly string UnknownPlayerStatusEffectStringId = "LOC_STATUS_EFFECT_UKNOWN";
        public static readonly string UnknownPlayerStringId = "LOC_UNKNOWN_PLAYER";

        public static string GetString(string stringId)
        {
            if (ConfigHelper.Localization.Values.Strings.TryGetValue(stringId, out var value))
            {
                return value;
            }

            Log.WriteLine($"Localization: '{stringId}' does not exist in {ConfigHelper.Localization.FileName}");

            return stringId;
        }

        public static string GetMonsterName(string monsterId)
        {
            if (ConfigHelper.MonsterData.Values.Monsters.TryGetValue(monsterId, out var monsterConfig))
            {
                return GetString(monsterConfig.NameStringId);
            }

            Log.WriteLine($"Localization: Monster '{monsterId}' not found in {ConfigHelper.MonsterData.FileName}");

            return GetString(UnknownMonsterStringId);
        }

        public static string GetMonsterPartName(string monsterId, int partIndex)
        {
            if (ConfigHelper.MonsterData.Values.Monsters.TryGetValue(monsterId, out var monsterConfig))
            {
                if (monsterConfig.PartStringIds != null && monsterConfig.PartStringIds.Length > partIndex)
                {
                    return GetString(monsterConfig.PartStringIds[partIndex]);
                }
                else
                {
                    Log.WriteLine($"Localization: Monster '{monsterId}' part '{partIndex}' not found in {ConfigHelper.MonsterData.FileName}");
                }
            }
            else
            {
                Log.WriteLine($"Localization: Monster '{monsterId}' not found in {ConfigHelper.MonsterData.FileName}");
            }

            return GetString(UnknownPartStringId);
        }

        public static string GetMonsterRemovablePartName(string monsterId, int removablePartIndex)
        {
            if (ConfigHelper.MonsterData.Values.Monsters.TryGetValue(monsterId, out var monsterConfig))
            {
                if (monsterConfig.RemovablePartStringIds != null && monsterConfig.RemovablePartStringIds.Length > removablePartIndex)
                {
                    return GetString(monsterConfig.RemovablePartStringIds[removablePartIndex]);
                }
                else
                {
                    Log.WriteLine($"Localization: Monster '{monsterId}' removable part '{removablePartIndex}' not found in {ConfigHelper.MonsterData.FileName}");
                }
            }
            else
            {
                Log.WriteLine($"Localization: Monster '{monsterId}' not found in {ConfigHelper.MonsterData.FileName}");
            }
            
            return GetString(UnknownRemovablePartStringId);
        }

        public static string GetMonsterStatusEffectName(int statusEffectId)
        {
            if (ConfigHelper.MonsterData.Values.MonsterStatusEffects.TryGetValue(statusEffectId, out var stringId))
            {
                return GetString(stringId);
            }

            Log.WriteLine($"Localization: Status effect '{statusEffectId}' not found in {ConfigHelper.MonsterData.FileName}");

            return GetString(UnknownMonsterStatusEffectStringId);
        }

        public static string GetPlayerStatusEffectName(int index)
        {
            if (ConfigHelper.PlayerData.Values.PlayerStatusEffects.Length > index)
            {
                return GetString(ConfigHelper.PlayerData.Values.PlayerStatusEffects[index].NameStringId);
            }

            Log.WriteLine($"Localization: Player status effect with index '{index}' not found in {ConfigHelper.PlayerData.FileName}");

            return GetString(UnknownPlayerStatusEffectStringId);
        }
    }
}