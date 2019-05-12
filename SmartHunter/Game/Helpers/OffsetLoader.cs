using Newtonsoft.Json;

namespace SmartHunter.Game.Helpers {
    public class MonsterOffsets {
        // Doubly linked list
        public readonly ulong PreviousMonsterPtr = 0x28;
        public readonly ulong NextMonsterPtr = 0x30;
        public readonly ulong SizeScale = 0x174;
        public readonly ulong ModelPtr = 0x290;
        public readonly ulong PartCollection = 0x129D8;
        public readonly ulong RemovablePartCollectionOffset = 0x1ED0;
        public readonly ulong StatusEffectCollection = 0x19900;

        [JsonIgnore]
        public ulong RemovablePartCollection => PartCollection + RemovablePartCollectionOffset;
    }

    public class MonsterModelOffsets {
        public readonly int IdLength = 64;
        public readonly ulong Id = 0x0C;
    }

    public class MonsterHealthComponentOffsets {
        public readonly ulong MaxHealth = 0x60;
        public readonly ulong CurrentHealth = 0x64;
    }

    public class MonsterPartCollectionOffsets {
        public readonly int MaxItemCount = 16;
        public readonly ulong HealthComponentPtr = 0x48;
        public readonly ulong FirstPart = 0x50;
    }

    public class MonsterPartOffsets {
        public readonly ulong MaxHealth = 0x0C;
        public readonly ulong CurrentHealth = 0x10;
        public readonly ulong TimesBrokenCount = 0x18;
        public readonly ulong NextPart = 0x1E8;
    }

    public class MonsterRemovablePartCollectionOffsets {
        public readonly int MaxItemCount = 32;
        public readonly ulong FirstRemovablePart = 0x08;
    }

    public class MonsterRemovablePartOffsets {
        public readonly ulong MaxHealth = 0x0C;
        public readonly ulong CurrentHealth = 0x10;
        public readonly ulong Validity1 = 0x14;
        public readonly ulong TimesBrokenCount = 0x18;
        public readonly ulong Validity2 = 0x28;
        public readonly ulong Validity3 = 0x40;
        public readonly ulong NextRemovablePart = 0x78;
    }

    public class MonsterStatusEffectCollectionOffsets {
        public int MaxItemCount = 20;
        public ulong NextStatusEffectPtr = 0x08;
    }

    public class MonsterStatusEffectOffsets {
        public readonly ulong Id = 0x158;
        public readonly ulong MaxDuration = 0x15C;
        public readonly ulong CurrentBuildup = 0x178;
        public readonly ulong MaxBuildup = 0x17C;
        public readonly ulong CurrentDuration = 0x1A4;
        public readonly ulong TimesActivatedCount = 0x1A8;
    }

    public class PlayerNameCollectionOffsets {
        public readonly int PlayerNameLength = 32 + 1; // +1 for null terminator
        public readonly ulong FirstPlayerName = 0x54A45;
    }

    public class PlayerDamageCollectionOffsets {
        public readonly int MaxPlayerCount = 4;
        public readonly ulong FirstPlayerPtr = 0x48;
        public readonly ulong NextPlayerPtr = 0x58;
    }

    public class PlayerDamageOffsets {
        public readonly ulong Damage = 0x48;
    }
}