using System.Collections.Generic;

namespace SmartHunter.Game.Config
{
    public class MonsterDataConfig
    {
        public Dictionary<string, MonsterConfig> Monsters = new Dictionary<string, MonsterConfig>()
        {
            {
                "em100_00",
                new MonsterConfig("LOC_MONSTER_ANJANATH",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1646.46f, 1f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em002_01",
                new MonsterConfig("LOC_MONSTER_AZURE_RATHALOS",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1704.22f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em044_00",
                new MonsterConfig("LOC_MONSTER_BARROTH",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_HEAD_MUD",
                    "LOC_PART_BODY",
                    "LOC_PART_BODY_MUD",
                    "LOC_PART_ARMS",
                    "LOC_PART_ARMS_MUD",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_LEFT_MUD",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_LEG_RIGHT_MUD",
                    "LOC_PART_TAIL",
                    "LOC_PART_TAIL_MUD"
                },
                1383.07f, 0.81f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em118_00",
                new MonsterConfig("LOC_MONSTER_BAZELGEUSE",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_LEGS",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_TAIL",
                },
                1928.38f, 1.1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em121_00",
                new MonsterConfig("LOC_MONSTER_BEHEMOTH",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HORNS",
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARM_LEFT",
                    "LOC_PART_ARM_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                3423.65f, 1f,
                null)
            },
            {
                "em007_01",
                new MonsterConfig("LOC_MONSTER_BLACK_DIABLOS",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                2096.25f, 1.2f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em043_00",
                new MonsterConfig("LOC_MONSTER_DEVILJHO",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_CHEST",
                    "LOC_PART_REAR",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                2063.82f, 1f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em007_00",
                new MonsterConfig("LOC_MONSTER_DIABLOS",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                2096.25f, 1.2f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em116_00",
                new MonsterConfig("LOC_MONSTER_DODOGAMA",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL"
                },
                1111.11f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em112_00",
                new MonsterConfig("LOC_MONSTER_GREAT_GIRROS",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL"
                },
                1053.15f, 0.9f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em101_00",
                new MonsterConfig("LOC_MONSTER_GREAT_JAGRAS",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL",
                    "LOC_PART_ABDOMEN"
                },
                1109.66f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em108_00",
                new MonsterConfig("LOC_MONSTER_JYURATODUS",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL",
                    "LOC_PART_HEAD_MUD",
                    "LOC_PART_BODY_MUD",
                    "LOC_PART_LEG_LEFT_MUD",
                    "LOC_PART_LEG_RIGHT_MUD",
                    "LOC_PART_TAIL_MUD"
                },
                1508.71f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em011_00",
                new MonsterConfig("LOC_MONSTER_KIRIN",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS"
                },
                536.26f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em107_00",
                new MonsterConfig("LOC_MONSTER_KULU_YA_KU",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL",
                    "LOC_PART_ROCK"
                },
                901.24f, 0.9f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "9985",
                new MonsterConfig("LOC_MONSTER_KULVE_TAROTH",
                new string[]
                {
                    "LOC_REMOVABLE_PART_HORNS",
                    "LOC_REMOVABLE_PART_HORNS_2"
                },
                new string[]
                {
                    "LOC_PART_HORNS",
                    "LOC_PART_CHEST",
                    "LOC_PART_BODY",
                    "LOC_PART_LIMBS_LEFT",
                    "LOC_PART_LIMBS_RIGHT",
                    "LOC_PART_TAIL",
                    "LOC_PART_HORNS_GOLD",
                    "LOC_PART_MANE_GOLD",
                    "LOC_PART_CHEST_LEFT_GOLD",
                    "LOC_PART_CHEST_RIGHT_GOLD",
                    "LOC_PART_ARM_LEFT_GOLD",
                    "LOC_PART_ARM_RIGHT_GOLD",
                    "LOC_PART_LEG_LEFT_GOLD",
                    "LOC_PART_LEG_RIGHT_GOLD",
                    "LOC_PART_TAIL_LEFT_GOLD",
                    "LOC_PART_TAIL_RIGHT_GOLD"
                },
                4573.25f, 1f,
                null)
            },
            {
                "em024_00",
                new MonsterConfig("LOC_MONSTER_KUSHALA_DAORA",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_TAIL",
                    "LOC_PART_LIMBS_LEFT",
                    "LOC_PART_LIMBS_RIGHT",
                    "LOC_PART_WINGS"
                },
                1913.13f, 0.85f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em036_00",
                new MonsterConfig("LOC_MONSTER_LAVASIOTH",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ABDOMEN",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1797.24f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em111_00",
                new MonsterConfig("LOC_MONSTER_LEGIANA",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1699.75f, 0.9f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em026_00",
                new MonsterConfig("LOC_MONSTER_LUNASTRA",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_LIMBS",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_TAIL"
                },
                1828.69f, 0.85f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em103_00",
                new MonsterConfig("LOC_MONSTER_NERGIGANTE",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HORNS",
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARM_LEFT",
                    "LOC_PART_ARM_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_TAIL"
                },
                1848.12f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em113_00",
                new MonsterConfig("LOC_MONSTER_ODOGARON",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL"
                },
                1388.75f, 1.15f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em110_00",
                new MonsterConfig("LOC_MONSTER_PAOLUMU",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BALLOON",
                    "LOC_PART_BODY",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_TAIL"
                },
                1143.36f, 1.05f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em001_01",
                new MonsterConfig("LOC_MONSTER_PINK_RATHIAN",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1754.37f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em102_00",
                new MonsterConfig("LOC_MONSTER_PUKEI_PUKEI",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1102.45f, 1f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em114_00",
                new MonsterConfig("LOC_MONSTER_RADOBAAN",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL",
                    "LOC_PART_JAW",
                    "LOC_PART_BACK",
                    "LOC_PART_BONE_LEFT",
                    "LOC_PART_BONE_RIGHT"
                },
                1803.47f, 0.9f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em002_00",
                new MonsterConfig("LOC_MONSTER_RATHALOS",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1704.22f, 1f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em001_00",
                new MonsterConfig("LOC_MONSTER_RATHIAN",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_WING_LEFT",
                    "LOC_PART_WING_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                1754.37f, 1f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em027_00",
                new MonsterConfig("LOC_MONSTER_TEOSTRA",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_WINGS",
                    "LOC_PART_TAIL"
                },
                1790.15f, 0.85f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em109_00",
                new MonsterConfig("LOC_MONSTER_TOBI_KADACHI",
                null,
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_MANE",
                    "LOC_PART_ABDOMEN",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL"
                },
                1300.52f, 1f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em120_00",
                new MonsterConfig("LOC_MONSTER_TZITZI_YA_KU",
                null,
                new string[]
                {
                    "LOC_PART_HEAD_LEFT",
                    "LOC_PART_HEAD_RIGHT",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEGS",
                    "LOC_PART_TAIL"
                },
                894.04f, 0.9f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em045_00",
                new MonsterConfig("LOC_MONSTER_URAGAAN",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_JAW",
                    "LOC_PART_HEAD",
                    "LOC_PART_BODY",
                    "LOC_PART_ARMS",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_TAIL"
                },
                2058.63f, 1f,
                new MonsterCrownConfig(CrownPreset.Alternate))
            },
            {
                "em115_00",
                new MonsterConfig("LOC_MONSTER_VAAL_HAZAK",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_BACK",
                    "LOC_PART_CHEST",
                    "LOC_PART_TAIL",
                    "LOC_PART_ARM_LEFT",
                    "LOC_PART_ARM_RIGHT",
                    "LOC_PART_LEG_LEFT",
                    "LOC_PART_LEG_RIGHT",
                    "LOC_PART_WINGS"
                },
                2095.4f, 0.9f,
                new MonsterCrownConfig(CrownPreset.Standard))
            },
            {
                "em105_00",
                new MonsterConfig("LOC_MONSTER_XENO_JIIVA",
                new string[]
                {
                    "LOC_REMOVABLE_PART_TAIL"
                },
                new string[]
                {
                    "LOC_PART_HEAD",
                    "LOC_PART_NECK",
                    "LOC_PART_BACK",
                    "LOC_PART_CHEST",
                    "LOC_PART_HAND_LEFT",
                    "LOC_PART_HAND_RIGHT",
                    "LOC_PART_FOOT_LEFT",
                    "LOC_PART_FOOT_RIGHT",
                    "LOC_PART_WINGS",
                    "LOC_PART_TAIL"
                },
                4509.1f, 1f,
                null)
            },
            {
                "em106_00",
                new MonsterConfig("LOC_MONSTER_ZORAH_MAGDAROS",
                null,
                null,
                25764.59f, 1f,
                null)
            }
        };

        public Dictionary<int, string> MonsterStatusEffects = new Dictionary<int, string>()
        {
            { 1, "LOC_STATUS_EFFECT_POISON" },
            { 2, "LOC_STATUS_EFFECT_PARALYSIS" },
            { 3, "LOC_STATUS_EFFECT_SLEEP" },
            { 4, "LOC_STATUS_EFFECT_BLAST" },
            { 5, "LOC_STATUS_EFFECT_MOUNT" },
            { 6, "LOC_STATUS_EFFECT_EXHAUST" },
            { 7, "LOC_STATUS_EFFECT_STUN" },
            { 8, "LOC_STATUS_EFFECT_TRANQUILIZE" },
            { 10, "LOC_STATUS_EFFECT_FLASH" },
            { 11, "LOC_STATUS_EFFECT_DUNG" }
        };
    }
}