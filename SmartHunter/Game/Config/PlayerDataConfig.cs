using SmartHunter.Game.Data;

namespace SmartHunter.Game.Config
{
    public class PlayerDataConfig
    {
        private static string indexToHexStr(int index, ulong baseOffset)
        {
            ulong multiplier = 0x4;
            string prefix = "";
            if (index < 0)
            {
                index = (-1) * index;
                prefix = "-";
            }
            ulong hex = baseOffset + multiplier * (ulong)index;
            return $"{prefix}{hex.ToString("X")}";
        }

        private static string indexToHexStrMantles(int index)
        {
            return indexToHexStr(index, 0xEFC);
        }

        private static string indexToHexStrMantlesRecharging(int index)
        {
            return indexToHexStr(index, 0xEAC);
        }

        private static string indexToHexStrNoOffset(int index)
        {
            return indexToHexStr(index, 0x0);
        }

        public StatusEffectConfig[] StatusEffects =
        {
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_SELF_IMPROVEMENT", (uint)(uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(14)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ATTACK_UP_S", (uint)StatusEffectConfig.MemorySource.Base,indexToHexStrNoOffset(15)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ATTACK_UP_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(16)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_BOOST_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(17)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_BOOST_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(18)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_STAMINA_USE_REDUCED_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(19)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_STAMINA_USE_REDUCED_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(20)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WIND_PRESSURE_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(21)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ALL_WIND_PRESSURE_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(22)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DEFENSE_UP_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(23)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DEFENSE_UP_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(24)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_TOOL_USE_DRAIN_REDUCED_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(25)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_TOOL_USE_DRAIN_REDUCED_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(26)),





            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_RECOVERY_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(32)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_RECOVERY_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(33)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_EARPLUGS_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(34)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_EARPLUGS_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(35)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DIVINE_PROTECTION", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(36)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_SCOUTFLY_POWER_UP", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(37)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ENVIRONMENTAL_DAMAGE_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(38)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_STUN_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(39)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_PARALYSIS_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(40)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_TREMORS_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(41)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_MUCK_RESISTANCE", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(42)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_FIRE_RESISTANCE_BOOST_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(43)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_FIRE_RESISTANCE_BOOST_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(44)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WATER_RESISTANCE_BOOST_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(45)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WATER_RESISTANCE_BOOST_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(46)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_THUNDER_RESISTANCE_BOOST_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(47)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_THUNDER_RESISTANCE_BOOST_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(48)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ICE_RESISTANCE_BOOST_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(49)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ICE_RESISTANCE_BOOST_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(50)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DRAGON_RESISTANCE_BOOST_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(51)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DRAGON_RESISTANCE_BOOST_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(52)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ELEMENTAL_ATTACK_BOOST", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(53)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_BLIGHT_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(54)),


            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_KNOCKBACKS_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(57)),

            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ELEMENTAL_RESISTANCE_UP", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(59)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_AFFINITY_UP", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(60)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ALL_AILMENTS_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(61)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WIND_PRESSURE_NEGATED_AND_EARPLUGS_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(62)),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ABNORMAL_STATUS_ATTACK_INCREASED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(63)),

            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_ATTACK_UP_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(72)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_ATTACK_UP_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(73)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_DEFENSE_UP_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(74)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_DEFENSE_UP_L", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(75)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_AFFINITY_UP", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(76)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_HEALTH_RECOVERY", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(77)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_HEALTH_BOOST", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(78)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_STAMINA_USE_REDUCED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(79)),

            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_DIVINE_PROTECTION", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(81)),

            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_STUN_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(83)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_PARALYSIS_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(84)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_TREMORS_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(85)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_EARPLUGS_S", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(86)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_WIND_PRESSURE_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(87)),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_ENVIRONMENTAL_DAMAGE_NEGATED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(88)),

            //statuses
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_POISON", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(375)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_VIRULENT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(376)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_FIREBLIGHT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(379)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_THUNDERBLIGHT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(380)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_WATERBLIGHT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(381)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_ICEBLIGHT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(382)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_DRAGONBLIGHT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(383)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLEEDING", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(384)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLEEDING_RECOVERY", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(385)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_EFFLUVIA", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(386)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_DEFENSE_DOWN", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(387)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_ELEMENTAL_RESISTANCE_DOWN", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(389)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_NO_ITEMS", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(391)),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLASTBLIGHT", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(392)),
            //BLASTSCOURGE Always Display?
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLASTSCOURGE", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(399)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_DASH_JUICE", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(420)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_WIGGLY_LITCHI", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(421)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_IMMUNIZER", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(422)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MIGHT_SEED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(424), new MemoryConditionConfig(10, indexToHexStrNoOffset(425))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MIGHT_PILL", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(424), new MemoryConditionConfig(25, indexToHexStrNoOffset(425))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ADAMANT_SEED", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(428), new MemoryConditionConfig(20, indexToHexStrNoOffset(429))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ADAMANT_PILL", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(428), new MemoryConditionConfig(0, indexToHexStrNoOffset(429))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_DEMON_POWDER", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(433)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_HARDSHELL_POWDER", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(434)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_DEMONDRUG", (uint)StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(1, indexToHexStrNoOffset(437))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MEGA_DEMONDRUG", (uint)StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(2, indexToHexStrNoOffset(437))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ARMORSKIN", (uint)StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(1, indexToHexStrNoOffset(438))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MEGA_ARMORSKIN", (uint)StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(2, indexToHexStrNoOffset(438))),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_COOL_DRINK", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(443)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_HOT_DRINK", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(444)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_HEALTH_RECOVERY(HOT_SPRING)", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(446)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_CLODPROOF(HOT_SPRING)", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(447)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ATTACK_UP(POWERCONE)", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(454)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ICEPROOF(THAWPUFF)", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(455)),

            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_PROTECTIVE_POLISH", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(475)),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_AFFINITY_SLIDING", (uint)StatusEffectConfig.MemorySource.Base, indexToHexStrNoOffset(476)),

            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_GHILLIE", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(0)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_TEMPORAL", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(1)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_HEALTH_BOOSTER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(2)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_ROCKSTEADY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(3)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_CHALLENGER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(4)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_VITALITY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(5)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_FIREPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(6)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_WATERPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(7)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_ICEPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(8)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_THUNDERPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(9)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_DRAGONPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(10)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_CLEANSER_BOOSTER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(11)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_GLIDER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(12)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_EVASION", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(13)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_IMPACT", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(14)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_APOTHECARY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(15)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_IMMUNITY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(16)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_AFFINITY_BOOSTER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(17)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_BANDIT", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(18)),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_ASSASSINS_HOOD", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantles(19)),

            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_GHILLIE", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(0), new MemoryConditionConfig(0, indexToHexStrMantles(0))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_TEMPORAL", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(1), new MemoryConditionConfig(0,indexToHexStrMantles(1))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_HEALTH_BOOSTER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(2), new MemoryConditionConfig(0, indexToHexStrMantles(2))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_ROCKSTEADY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(3), new MemoryConditionConfig(0, indexToHexStrMantles(3))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_CHALLENGER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(4), new MemoryConditionConfig(0, indexToHexStrMantles(4))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_VITALITY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(5), new MemoryConditionConfig(0, indexToHexStrMantles(5))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_FIREPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(6), new MemoryConditionConfig(0, indexToHexStrMantles(6))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_WATERPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(7), new MemoryConditionConfig(0, indexToHexStrMantles(7))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_ICEPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(8), new MemoryConditionConfig(0, indexToHexStrMantles(8))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_THUNDERPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(9), new MemoryConditionConfig(0, indexToHexStrMantles(9))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_DRAGONPROOF", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(10), new MemoryConditionConfig(0, indexToHexStrMantles(10))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_CLEANSER_BOOSTER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(11), new MemoryConditionConfig(0, indexToHexStrMantles(11))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_GLIDER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(12), new MemoryConditionConfig(0, indexToHexStrMantles(12))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_EVASION", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(13), new MemoryConditionConfig(0, indexToHexStrMantles(13))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_IMPACT", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(14), new MemoryConditionConfig(0, indexToHexStrMantles(14))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_APOTHECARY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(15),  new MemoryConditionConfig(0, indexToHexStrMantles(15))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_IMMUNITY", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(16), new MemoryConditionConfig(0, indexToHexStrMantles(16))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_AFFINITY_BOOSTER", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(17), new MemoryConditionConfig(0, indexToHexStrMantles(17))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_MANTLE_BANDIT", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(18), new MemoryConditionConfig(0, indexToHexStrMantles(18))),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_RECHARGE_ASSASSINS_HOOD", (uint)StatusEffectConfig.MemorySource.Equipment, indexToHexStrMantlesRecharging(19), new MemoryConditionConfig(0, indexToHexStrMantles(19))),

            // Not working
            /*
            new StatusEffectConfig("Weapon", "LOC_WEAPON_INSECT_GLAIVE_ATTACK", (uint)StatusEffectConfig.MemorySource.Weapon, "1FE8", WeaponType.INSECT_GLAIVE),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_INSECT_GLAIVE_SPEED", (uint)StatusEffectConfig.MemorySource.Weapon, "1FEC", WeaponType.INSECT_GLAIVE),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_INSECT_GLAIVE_DEFENSE", (uint)StatusEffectConfig.MemorySource.Weapon, "1FF0", WeaponType.INSECT_GLAIVE),
            */
            // Working

            new StatusEffectConfig("Weapon", "LOC_WEAPON_LONGSWORD_SPIRIT_GAUGE_REGEN_HELM_BREAKER", (uint)WeaponType.LONG_SWORD, indexToHexStrNoOffset(2402)),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_LONGSWORD_SPIRIT_GAUGE_REGEN_LAI_SLASH", (uint)WeaponType.LONG_SWORD, indexToHexStrNoOffset(2406)),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_LONGSWORD_STEADY_SPIRIT_LEVEL", (uint)WeaponType.LONG_SWORD, indexToHexStrNoOffset(2408)),

            // Not Working
            /*
            new StatusEffectConfig("Weapon", "LOC_WEAPON_CHARGE_BLADE_SHIELD_CHARGE", (uint)StatusEffectConfig.MemorySource.Weapon, "1FF8", WeaponType.CHARGE_BLADE),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_CHARGE_BLADE_BLADE_CHARGE", (uint)StatusEffectConfig.MemorySource.Weapon, "1FFC", WeaponType.CHARGE_BLADE),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_SWITCH_AXE_AMPED_STATE", (uint)StatusEffectConfig.MemorySource.Weapon, "1FD4", WeaponType.SWITCH_AXE),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_HAMMER_POWER_CHARGE", (uint)StatusEffectConfig.MemorySource.Weapon, null, WeaponType.HAMMER, new MemoryConditionConfig((byte)1, "1FC4"))
            */
            };
    }
}
