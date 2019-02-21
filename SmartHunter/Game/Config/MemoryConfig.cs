namespace SmartHunter.Game.Config
{
    public class MemoryConfig
    {
        public string ProcessName = "MonsterHunterWorld";

        public int ThreadsPerScan = 4;

        public string AddressRangeStart = "140004000";
        public string AddressRangeEnd = "145000000";

        public string PlayerDamagePattern = "48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B D8 48 85 C0 75 04 33 C9";
        public string PlayerNamePattern = "48 8B 0D ?? ?? ?? ?? 48 8D 54 24 38 C6 44 24 20 00 4D 8B 40 08 E8 ?? ?? ?? ?? 48 8B 5C 24 60 48 83 C4 50 5F C3";
        public string MonsterPattern = "48 8B 05 ?? ?? ?? ?? 8B 48 ?? 89 8F ?? ?? ?? ??";
        public string PlayerBuffPattern = "48 8B 05 ?? ?? ?? ?? 41 8B 94 00 ?? ?? ?? ?? 89 51 ??";
    }
}
