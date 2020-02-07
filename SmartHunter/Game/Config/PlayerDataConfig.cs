namespace SmartHunter.Game.Config
{
    public class PlayerDataConfig
    {
        public StatusEffectConfig[] StatusEffects =
        {
            /*The commented ones needs the offset to be updated (ergo, are NOT working right now)
             *
             * 
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_SELF_IMPROVEMENT", StatusEffectConfig.MemorySource.Base, "38"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ATTACK_UP_S", StatusEffectConfig.MemorySource.Base, "3C"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ATTACK_UP_L", StatusEffectConfig.MemorySource.Base, "40"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_BOOST_S", StatusEffectConfig.MemorySource.Base, "44"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_BOOST_L", StatusEffectConfig.MemorySource.Base, "48"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_STAMINA_USE_REDUCED_S", StatusEffectConfig.MemorySource.Base, "4C"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_STAMINA_USE_REDUCED_L", StatusEffectConfig.MemorySource.Base, "50"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WIND_PRESSURE_NEGATED", StatusEffectConfig.MemorySource.Base, "54"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ALL_WIND_PRESSURE_NEGATED", StatusEffectConfig.MemorySource.Base, "58"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DEFENSE_UP_S", StatusEffectConfig.MemorySource.Base, "5C"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DEFENSE_UP_L", StatusEffectConfig.MemorySource.Base, "60"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_TOOL_USE_DRAIN_REDUCED_S", StatusEffectConfig.MemorySource.Base, "64"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_TOOL_USE_DRAIN_REDUCED_L", StatusEffectConfig.MemorySource.Base, "68"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_RECOVERY_S", StatusEffectConfig.MemorySource.Base, "80"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_HEALTH_RECOVERY_L", StatusEffectConfig.MemorySource.Base, "84"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_EARPLUGS_S", StatusEffectConfig.MemorySource.Base, "88"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_EARPLUGS_L", StatusEffectConfig.MemorySource.Base, "8C"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DIVINE_PROTECTION", StatusEffectConfig.MemorySource.Base, "90"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_SCOUTFLY_POWER_UP", StatusEffectConfig.MemorySource.Base, "94"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ENVIRONMENTAL_DAMAGE_NEGATED", StatusEffectConfig.MemorySource.Base, "98"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_STUN_NEGATED", StatusEffectConfig.MemorySource.Base, "9C"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_PARALYSIS_NEGATED", StatusEffectConfig.MemorySource.Base, "A0"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_TREMORS_NEGATED", StatusEffectConfig.MemorySource.Base, "A4"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_MUCK_RESISTANCE", StatusEffectConfig.MemorySource.Base, "A8"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_FIRE_RESISTANCE_BOOST_S", StatusEffectConfig.MemorySource.Base, "AC"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_FIRE_RESISTANCE_BOOST_L", StatusEffectConfig.MemorySource.Base, "B0"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WATER_RESISTANCE_BOOST_S", StatusEffectConfig.MemorySource.Base, "B4"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WATER_RESISTANCE_BOOST_L", StatusEffectConfig.MemorySource.Base, "B8"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_THUNDER_RESISTANCE_BOOST_S", StatusEffectConfig.MemorySource.Base, "BC"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_THUNDER_RESISTANCE_BOOST_L", StatusEffectConfig.MemorySource.Base, "C0"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ICE_RESISTANCE_BOOST_S", StatusEffectConfig.MemorySource.Base, "C4"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ICE_RESISTANCE_BOOST_L", StatusEffectConfig.MemorySource.Base, "C8"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DRAGON_RESISTANCE_BOOST_S", StatusEffectConfig.MemorySource.Base, "CC"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_DRAGON_RESISTANCE_BOOST_L", StatusEffectConfig.MemorySource.Base, "D0"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ELEMENTAL_ATTACK_BOOST", StatusEffectConfig.MemorySource.Base, "D4"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_BLIGHT_NEGATED", StatusEffectConfig.MemorySource.Base, "D8"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_KNOCKBACKS_NEGATED", StatusEffectConfig.MemorySource.Base, "E4"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ELEMENTAL_RESISTANCE_UP", StatusEffectConfig.MemorySource.Base, "EC"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_AFFINITY_UP", StatusEffectConfig.MemorySource.Base, "F0"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ALL_AILMENTS_NEGATED", StatusEffectConfig.MemorySource.Base, "F4"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_WIND_PRESSURE_NEGATED_AND_EARPLUGS_S", StatusEffectConfig.MemorySource.Base, "F8"),
            new StatusEffectConfig("Horn", "LOC_STATUS_EFFECT_ABNORMAL_STATUS_ATTACK_INCREASED", StatusEffectConfig.MemorySource.Base, "FC"),

            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_ATTACK_UP_S", StatusEffectConfig.MemorySource.Base, "104"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_ATTACK_UP_L", StatusEffectConfig.MemorySource.Base, "108"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_DEFENSE_UP_S", StatusEffectConfig.MemorySource.Base, "10C"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_DEFENSE_UP_L", StatusEffectConfig.MemorySource.Base, "110"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_AFFINITY_UP", StatusEffectConfig.MemorySource.Base, "114"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_HEALTH_RECOVERY", StatusEffectConfig.MemorySource.Base, "118"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_HEALTH_BOOST", StatusEffectConfig.MemorySource.Base, "11C"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_STAMINA_USE_REDUCED", StatusEffectConfig.MemorySource.Base, "120"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_DIVINE_PROTECTION", StatusEffectConfig.MemorySource.Base, "128"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_STUN_NEGATED", StatusEffectConfig.MemorySource.Base, "130"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_PARALYSIS_NEGATED", StatusEffectConfig.MemorySource.Base, "134"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_TREMORS_NEGATED", StatusEffectConfig.MemorySource.Base, "138"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_EARPLUGS_S", StatusEffectConfig.MemorySource.Base, "13C"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_WIND_PRESSURE_NEGATED", StatusEffectConfig.MemorySource.Base, "140"),
            new StatusEffectConfig("Coral", "LOC_STATUS_EFFECT_ENVIRONMENTAL_DAMAGE_NEGATED", StatusEffectConfig.MemorySource.Base, "144"),

            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_POISON", StatusEffectConfig.MemorySource.Base, "554"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_FIREBLIGHT", StatusEffectConfig.MemorySource.Base, "55C"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_THUNDERBLIGHT", StatusEffectConfig.MemorySource.Base, "560"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_WATERBLIGHT", StatusEffectConfig.MemorySource.Base, "564"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_ICEBLIGHT", StatusEffectConfig.MemorySource.Base, "568"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_DRAGONBLIGHT", StatusEffectConfig.MemorySource.Base, "56C"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLEEDING", StatusEffectConfig.MemorySource.Base, "570"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLEEDING_RECOVERY", StatusEffectConfig.MemorySource.Base, "574"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_EFFLUVIA", StatusEffectConfig.MemorySource.Base, "578"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_DEFENSE_DOWN", StatusEffectConfig.MemorySource.Base, "57C"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_ELEMENTAL_RESISTANCE_DOWN", StatusEffectConfig.MemorySource.Base, "584"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_NO_ITEMS", StatusEffectConfig.MemorySource.Base, "58C"),
            new StatusEffectConfig("Debuff", "LOC_STATUS_EFFECT_BLASTBLIGHT", StatusEffectConfig.MemorySource.Base, "590"),
            */

            // To test

            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_DASH_JUICE", StatusEffectConfig.MemorySource.Base, "694"),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_WIGGLY_LITCHI", StatusEffectConfig.MemorySource.Base, "698"),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_IMMUNIZER", StatusEffectConfig.MemorySource.Base, "69C"),

            // Working

            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MIGHT_SEED", StatusEffectConfig.MemorySource.Base, "6A4", new MemoryConditionConfig(0, "6AC")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MIGHT_PILL", StatusEffectConfig.MemorySource.Base, "6A4", new MemoryConditionConfig(1, "6AC")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ADAMANT_SEED", StatusEffectConfig.MemorySource.Base, "6B4", new MemoryConditionConfig(20, "6B8")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ADAMANT_PILL", StatusEffectConfig.MemorySource.Base, "6B4", new MemoryConditionConfig(0, "6B8")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_DEMON_POWDER", StatusEffectConfig.MemorySource.Base, "6C8"),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_HARDSHELL_POWDER", StatusEffectConfig.MemorySource.Base, "6CC"),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_DEMONDRUG", StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(1, "6DE")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MEGA_DEMONDRUG", StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(2, "6DE")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_ARMORSKIN", StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(1, "6D8")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_MEGA_ARMORSKIN", StatusEffectConfig.MemorySource.Base, null, new MemoryConditionConfig(2, "6D8")),
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_COOL_DRINK", StatusEffectConfig.MemorySource.Base, "6F0"), // 6DC -> 6F0
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_HOT_DRINK", StatusEffectConfig.MemorySource.Base, "6F4"),

            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_GHILLIE", StatusEffectConfig.MemorySource.Equipment, "0"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_TEMPORAL", StatusEffectConfig.MemorySource.Equipment, "4"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_HEALTH_BOOSTER", StatusEffectConfig.MemorySource.Equipment, "8"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_ROCKSTEADY", StatusEffectConfig.MemorySource.Equipment, "C"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_CHALLENGER", StatusEffectConfig.MemorySource.Equipment, "10"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_VITALITY", StatusEffectConfig.MemorySource.Equipment, "14"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_FIREPROOF", StatusEffectConfig.MemorySource.Equipment, "18"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_WATERPROOF", StatusEffectConfig.MemorySource.Equipment, "1C"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_ICEPROOF", StatusEffectConfig.MemorySource.Equipment, "20"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_THUNDERPROOF", StatusEffectConfig.MemorySource.Equipment, "24"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_DRAGONPROOF", StatusEffectConfig.MemorySource.Equipment, "28"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_CLEANSER_BOOSTER", StatusEffectConfig.MemorySource.Equipment, "2C"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_GLIDER", StatusEffectConfig.MemorySource.Equipment, "30"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_EVASION", StatusEffectConfig.MemorySource.Equipment, "34"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_IMPACT", StatusEffectConfig.MemorySource.Equipment, "38"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_APOTHECARY", StatusEffectConfig.MemorySource.Equipment, "3C"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_IMMUNITY", StatusEffectConfig.MemorySource.Equipment, "40"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_AFFINITY_BOOSTER", StatusEffectConfig.MemorySource.Equipment, "44"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_BANDIT", StatusEffectConfig.MemorySource.Equipment, "48"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_ASSASSINS_HOOD", StatusEffectConfig.MemorySource.Equipment, "4C"),
            

            /*
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_PROTECTIVE_POLISH", StatusEffectConfig.MemorySource.Base, "644"), //DUNNO
            new StatusEffectConfig("Buff", "LOC_STATUS_EFFECT_AFFINITY_SLIDING", StatusEffectConfig.MemorySource.Base, "648"), //DUNNO
            
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_GHILLIE", StatusEffectConfig.MemorySource.Equipment, "A8C"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_TEMPORAL", StatusEffectConfig.MemorySource.Equipment, "A90"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_ROCKSTEADY", StatusEffectConfig.MemorySource.Equipment, "A98"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_CHALLENGER", StatusEffectConfig.MemorySource.Equipment, "A9C"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_VITALITY", StatusEffectConfig.MemorySource.Equipment, "AA0"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_FIREPROOF", StatusEffectConfig.MemorySource.Equipment, "AA4"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_WATERPROOF", StatusEffectConfig.MemorySource.Equipment, "AA8"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_ICEPROOF", StatusEffectConfig.MemorySource.Equipment, "AAC"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_THUNDERPROOF", StatusEffectConfig.MemorySource.Equipment, "AB0"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_DRAGONPROOF", StatusEffectConfig.MemorySource.Equipment, "AB4"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_GLIDER", StatusEffectConfig.MemorySource.Equipment, "ABC"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_EVASION", StatusEffectConfig.MemorySource.Equipment, "AC0"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_IMPACT", StatusEffectConfig.MemorySource.Equipment, "AC4"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_APOTHECARY", StatusEffectConfig.MemorySource.Equipment, "AC8"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_IMMUNITY", StatusEffectConfig.MemorySource.Equipment, "ACC"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_MANTLE_BANDIT", StatusEffectConfig.MemorySource.Equipment, "AD4"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_ASSASSINS_HOOD", StatusEffectConfig.MemorySource.Equipment, "AD8"),
            new StatusEffectConfig("Equipment", "LOC_EQUIPMENT_AFFINITY_BOOSTER", StatusEffectConfig.MemorySource.Equipment, "B68"),

            new StatusEffectConfig("Weapon", "LOC_WEAPON_INSECT_GLAIVE_ATTACK", StatusEffectConfig.MemorySource.Weapon, "1FE8", new MemoryConditionConfig(".*rod[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_INSECT_GLAIVE_SPEED", StatusEffectConfig.MemorySource.Weapon, "1FEC", new MemoryConditionConfig(".*rod[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_INSECT_GLAIVE_DEFENSE", StatusEffectConfig.MemorySource.Weapon, "1FF0", new MemoryConditionConfig(".*rod[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_LONGSWORD_SPIRIT_GAUGE_REGEN", StatusEffectConfig.MemorySource.Weapon, "1FF8", new MemoryConditionConfig(".*swo[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_LONGSWORD_STEADY_SPIRIT_LEVEL", StatusEffectConfig.MemorySource.Weapon, "2008", new MemoryConditionConfig(".*swo[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_CHARGE_BLADE_SHIELD_CHARGE", StatusEffectConfig.MemorySource.Weapon, "1FF8", new MemoryConditionConfig(".*caxe[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_CHARGE_BLADE_BLADE_CHARGE", StatusEffectConfig.MemorySource.Weapon, "1FFC", new MemoryConditionConfig(".*caxe[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_SWITCH_AXE_AMPED_STATE", StatusEffectConfig.MemorySource.Weapon, "1FD4", new MemoryConditionConfig(".*saxe[0-9]", "290", "3E0", "10")),
            new StatusEffectConfig("Weapon", "LOC_WEAPON_HAMMER_POWER_CHARGE", StatusEffectConfig.MemorySource.Weapon, null, new MemoryConditionConfig((byte)1, "1FC4"), new MemoryConditionConfig(".*ham[0-9]", "290", "3E0", "10"))
            */
        };
    }
}
