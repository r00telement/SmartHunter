using SmartHunter.Core;

namespace SmartHunter.Game.Config
{
    public class MemoryConfig
    {
        public string ProcessName = "MonsterHunterWorld";

        public int ThreadsPerScan = 1;

        public BytePatternConfig PlayerDamagePattern = new BytePatternConfig(
            "48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B D8 48 85 C0 75 04 33 C9",
            "140000000",
            "145000000",
            WindowsApi.RegionPageProtection.PAGE_EXECUTE_READ
            );

        public BytePatternConfig PlayerNamePattern = new BytePatternConfig(
            "48 8B 0D ?? ?? ?? ?? 48 8D 54 24 38 C6 44 24 20 00 4D 8B 40 08 E8 ?? ?? ?? ?? 48 8B 5C 24 60 48 83 C4 50 5F C3",
            "140000000",
            "145000000",
            WindowsApi.RegionPageProtection.PAGE_EXECUTE_READ
            );

        public BytePatternConfig MonsterPattern = new BytePatternConfig(
            "48 8B 05 ?? ?? ?? ?? 8B 48 ?? 89 8F ?? ?? ?? ??",
            "140000000",
            "145000000",
            WindowsApi.RegionPageProtection.PAGE_EXECUTE_READ
            );

        public BytePatternConfig MonsterOffsetPattern = new BytePatternConfig(
            "48 8B 8B ?? ?? ?? ?? 48 8B 01 FF 50 ?? 48 8B 8B ?? ?? ?? ?? E8 ?? ?? ?? ??  48 8B 8B ?? ?? ?? ?? B2 01 E8 ?? ?? ?? ??",
            "140000000",
            "145000000",
            WindowsApi.RegionPageProtection.PAGE_EXECUTE_READ
            );

        public BytePatternConfig PlayerBuffPattern = new BytePatternConfig(
            "48 8B 05 ?? ?? ?? ?? 41 8B 94 00 ?? ?? ?? ?? 89 51 ??",
            "140000000",
            "145000000",
            WindowsApi.RegionPageProtection.PAGE_EXECUTE_READ
            );
    }
}
