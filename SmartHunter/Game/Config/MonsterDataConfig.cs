using System.Collections.Generic;
using System.Linq;

namespace SmartHunter.Game.Config
{
    public class MonsterDataConfig
    {
        static class Tags
        {
            public static string[] RemovableTail = { "Tail", "Removable" };
            public static string[] Head = { "Head" };
            public static string[] Body = { "Body" };
            public static string[] Legs = { "Legs", "Limbs" };
            public static string[] Tail = { "Tail" };
            public static string[] Wings = { "Wings" };
            public static string[] Arms = { "Arms", "Limbs" };
            public static string[] Horns = { "Horns" };
            public static string[] RemovableHorns = { "Horns", "Removable" };
            public static string[] Limbs = { "Limbs" };
            public static string[] Mud = { "Mud" };
        }

        public Dictionary<string, MonsterConfig> Monsters = new Dictionary<string, MonsterConfig>()
        {
            {
                "em100_00",
                new MonsterConfig("LOC_MONSTER_ANJANATH",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail)
                    },
                    1646.46f, 1f,
                    new MonsterCrownConfig(CrownPreset.Alternate)
                )
            },
            {
                "em002_01",
                new MonsterConfig("LOC_MONSTER_AZURE_RATHALOS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail)
                    },
                    1704.22f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em044_00",
                new MonsterConfig("LOC_MONSTER_BARROTH",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_HEAD_MUD", false, Tags.Head.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_BODY_MUD", false, Tags.Body.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_ARMS_MUD", false, Tags.Arms.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT_MUD", false, Tags.Legs.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT_MUD", false, Tags.Legs.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_TAIL_MUD", false, Tags.Tail.Union(Tags.Mud))
                    },
                    1383.07f, 0.81f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em118_00",
                new MonsterConfig("LOC_MONSTER_BAZELGEUSE",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1928.38f, 1.1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em121_00",
                new MonsterConfig("LOC_MONSTER_BEHEMOTH",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HORNS", false, Tags.Horns),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARM_LEFT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_ARM_RIGHT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    3423.65f, 1f,
                    null
                )
            },
            {
                "em007_01",
                new MonsterConfig("LOC_MONSTER_BLACK_DIABLOS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail)
                    },
                    2096.25f, 1.2f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em043_00",
                new MonsterConfig("LOC_MONSTER_DEVILJHO",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_CHEST", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_REAR", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    2063.82f, 1f,
                    new MonsterCrownConfig(CrownPreset.Alternate)
                )
            },
            {
                "em007_00",
                new MonsterConfig("LOC_MONSTER_DIABLOS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail)
                    },
                    2096.25f, 1.2f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em116_00",
                new MonsterConfig("LOC_MONSTER_DODOGAMA",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1111.11f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em112_00",
                new MonsterConfig("LOC_MONSTER_GREAT_GIRROS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1053.15f, 0.9f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em101_00",
                new MonsterConfig("LOC_MONSTER_GREAT_JAGRAS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_ABDOMEN", false, Tags.Body),
                    },
                    1109.66f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em108_00",
                new MonsterConfig("LOC_MONSTER_JYURATODUS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_HEAD_MUD", false, Tags.Head.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_BODY_MUD", false, Tags.Body.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT_MUD", false, Tags.Legs.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT_MUD", false, Tags.Legs.Union(Tags.Mud)),
                        new MonsterPartConfig("LOC_PART_TAIL_MUD", false, Tags.Tail.Union(Tags.Mud)),
                    },
                    1508.71f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em011_00",
                new MonsterConfig("LOC_MONSTER_KIRIN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                    },
                    536.26f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em107_00",
                new MonsterConfig("LOC_MONSTER_KULU_YA_KU",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_ROCK", false, Tags.Body), // TODO: Tag?
                    },
                    901.24f, 0.9f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em117_00",
                new MonsterConfig("LOC_MONSTER_KULVE_TAROTH",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_HORNS", true, Tags.RemovableHorns),
                        new MonsterPartConfig("LOC_REMOVABLE_PART_HORNS_2", true, Tags.RemovableHorns),
                        new MonsterPartConfig("LOC_PART_HORNS", false, Tags.Horns),
                        new MonsterPartConfig("LOC_PART_CHEST", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LIMBS_LEFT", false, Tags.Limbs),
                        new MonsterPartConfig("LOC_PART_LIMBS_RIGHT", false, Tags.Limbs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_HORNS_GOLD", false, Tags.Horns),
                        new MonsterPartConfig("LOC_PART_MANE_GOLD", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_CHEST_LEFT_GOLD", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_CHEST_RIGHT_GOLD", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARM_LEFT_GOLD", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_ARM_RIGHT_GOLD", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT_GOLD", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT_GOLD", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL_LEFT_GOLD", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_TAIL_RIGHT_GOLD", false, Tags.Tail),
                    },
                    4573.25f, 1f,
                    null
                )
            },
            {
                "em024_00",
                new MonsterConfig("LOC_MONSTER_KUSHALA_DAORA",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_LIMBS_LEFT", false, Tags.Limbs),
                        new MonsterPartConfig("LOC_PART_LIMBS_RIGHT", false, Tags.Limbs),
                        new MonsterPartConfig("LOC_PART_WINGS", false, Tags.Wings),
                    },
                    1913.13f, 0.85f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em036_00",
                new MonsterConfig("LOC_MONSTER_LAVASIOTH",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ABDOMEN", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1797.24f, 1.2f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em111_00",
                new MonsterConfig("LOC_MONSTER_LEGIANA",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1699.75f, 0.9f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em026_00",
                new MonsterConfig("LOC_MONSTER_LUNASTRA",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LIMBS", false, Tags.Limbs),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1828.69f, 0.85f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em103_00",
                new MonsterConfig("LOC_MONSTER_NERGIGANTE",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HORNS", false, Tags.Horns),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARM_LEFT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_ARM_RIGHT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1848.12f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em113_00",
                new MonsterConfig("LOC_MONSTER_ODOGARON",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1388.75f, 1.15f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em110_00",
                new MonsterConfig("LOC_MONSTER_PAOLUMU",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BALLOON", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1143.36f, 1.05f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em001_01",
                new MonsterConfig("LOC_MONSTER_PINK_RATHIAN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1754.37f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em102_00",
                new MonsterConfig("LOC_MONSTER_PUKEI_PUKEI",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1102.45f, 1f,
                    new MonsterCrownConfig(CrownPreset.Alternate)
                )
            },
            {
                "em114_00",
                new MonsterConfig("LOC_MONSTER_RADOBAAN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_JAW", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BACK", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_BONE_LEFT", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_BONE_RIGHT", false, Tags.Body),
                    },
                    1803.47f, 0.9f,
                    new MonsterCrownConfig(CrownPreset.Alternate)
                )
            },
            {
                "em002_00",
                new MonsterConfig("LOC_MONSTER_RATHALOS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1704.22f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em001_00",
                new MonsterConfig("LOC_MONSTER_RATHIAN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WING_LEFT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_WING_RIGHT", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1754.37f, 1f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em027_00",
                new MonsterConfig("LOC_MONSTER_TEOSTRA",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_WINGS", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1790.15f, 0.85f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em109_00",
                new MonsterConfig("LOC_MONSTER_TOBI_KADACHI",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_MANE", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ABDOMEN", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    1300.52f, 1f,
                    new MonsterCrownConfig(CrownPreset.Alternate)
                )
            },
            {
                "em120_00",
                new MonsterConfig("LOC_MONSTER_TZITZI_YA_KU",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD_LEFT", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_HEAD_RIGHT", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    894.04f, 0.9f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em045_00",
                new MonsterConfig("LOC_MONSTER_URAGAAN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_JAW", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    2058.63f, 1f,
                    new MonsterCrownConfig(CrownPreset.Alternate)
                )
            },
            {
                "em115_00",
                new MonsterConfig("LOC_MONSTER_VAAL_HAZAK",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BACK", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_CHEST", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_ARM_LEFT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_ARM_RIGHT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_WINGS", false, Tags.Wings),
                    },
                    2095.4f, 0.9f,
                    new MonsterCrownConfig(CrownPreset.Standard)
                )
            },
            {
                "em105_00",
                new MonsterConfig("LOC_MONSTER_XENO_JIIVA",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_TAIL", true, Tags.RemovableTail),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_NECK", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_BACK", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_CHEST", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_HAND_LEFT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_HAND_RIGHT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_FOOT_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_FOOT_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_WINGS", false, Tags.Wings),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                    },
                    4509.1f, 1f,
                    null
                )
            },
            {
                "em106_00",
                new MonsterConfig("LOC_MONSTER_ZORAH_MAGDAROS",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_CHEST", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_ARM_LEFT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_ARM_RIGHT", false, Tags.Arms),
                        new MonsterPartConfig("LOC_PART_LEG_LEFT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_LEG_RIGHT", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_TAIL", false, Tags.Tail),
                        new MonsterPartConfig("LOC_PART_SHELL", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_EXHAUST_ORGAN_CENTRAL", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_EXHAUST_ORGAN_HEAD", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_EXHAUST_ORGAN_CRATER", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_EXHAUST_ORGAN_REAR", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WEAK_SHELL_LEFT", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_WEAK_SHELL_RIGHT", false, Tags.Body),
                    },
                    25764.59f, 1f,
                    null
                )
            },
            {
                "em127_00",
                new MonsterConfig("LOC_MONSTER_LESHEN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_ANTLER_LEFT", true, Tags.RemovableHorns),
                        new MonsterPartConfig("LOC_REMOVABLE_PART_ANTLER_RIGHT", true, Tags.RemovableHorns),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                    },
                    549.70f, 1f,
                    null
                )
            },
            {
                "em127_01",
                new MonsterConfig("LOC_MONSTER_ANCIENT_LESHEN",
                    new MonsterPartConfig[]
                    {
                        new MonsterPartConfig("LOC_REMOVABLE_PART_ANTLER_LEFT", true, Tags.RemovableHorns),
                        new MonsterPartConfig("LOC_REMOVABLE_PART_ANTLER_RIGHT", true, Tags.RemovableHorns),
                        new MonsterPartConfig("LOC_PART_HEAD", false, Tags.Head),
                        new MonsterPartConfig("LOC_PART_BODY", false, Tags.Body),
                        new MonsterPartConfig("LOC_PART_LEGS", false, Tags.Legs),
                        new MonsterPartConfig("LOC_PART_ARMS", false, Tags.Arms),
                    },
                    549.70f, 1f,
                    null
                )
            }
        };
        
        public MonsterStatusEffectConfig[] StatusEffects =
        {
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_POISON",       "08", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_PARALYSIS",    "10", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_SLEEP",        "18", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_BLAST",        "20", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_MOUNT",        "28", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_EXHAUST",      "30", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_STUN",         "38", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_TRANQUILIZE",  "40", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_FLASH",        "48", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_DUNG",         "58", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_SHOCK_TRAP",   "68", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_PITFALL_TRAP", "70", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_ELDERSEAL",    "88", "15C", "178", "17C", "1A4", "1A8"),
            new MonsterStatusEffectConfig("Rage",         "LOC_STATUS_EFFECT_RAGE",         null, "190", "180", "1A0", "18C", "198"),
            new MonsterStatusEffectConfig("StatusEffect", "LOC_STATUS_EFFECT_FATIGUE",      null, "42C", "420", "424", "430",  null, true),
        };
    }
}