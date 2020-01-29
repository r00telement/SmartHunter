using SmartHunter.Core;

namespace SmartHunter.Game.Config
{
    public class MemoryConfig
    {
        public string ProcessName = "MonsterHunterWorld";

        public int ThreadsPerScan = 1;

        public BytePatternConfig PlayerDamagePattern = new BytePatternConfig(
            "PlayerDamagePattern",
            "48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B D8 48 85 C0 75 04 33 C9",
            "140000000",
            "163B0A000",
            WindowsApi.RegionPageProtection.PAGE_READONLY
            );

        public BytePatternConfig PlayerNamePattern = new BytePatternConfig(
            "PlayerNamePattern",
            "48 8B 0D ?? ?? ?? ?? 48 8D 54 24 38 C6 44 24 20 00 E8 ?? ?? ?? ?? 48 8B 5C 24 70 48 8B 7C 24 60 48 83 C4 68 C3",
            //"48 8B 0D ?? ?? ?? ?? 48 8D 54 24 38 C6 44 24 20 00 E8 ?? ?? ?? ?? 48 8B 5C 24 70 48 48 83 C4 60 5F C3",
            "140000000",
            "163B0A000",
            WindowsApi.RegionPageProtection.PAGE_READONLY
            );

        public BytePatternConfig MonsterPattern = new BytePatternConfig(
            "MonsterPattern",
            /*"48 8B 05 ?? ?? ?? ?? 8B 48 ?? 89 8F ?? ?? ?? ??",*/
            "48 8B 0D ?? ?? ?? ?? B2 01 E8 ?? ?? ?? ?? C6 83 ?? ?? ?? ?? ?? 48 8B 0D",
            "140000000",
            "163B0A000",
            WindowsApi.RegionPageProtection.PAGE_READONLY
            );

        public BytePatternConfig PlayerBuffPattern = new BytePatternConfig(
            "PlayerBuffPattern",
            //"48 8B 05 ?? ?? ?? ?? 41 8B 94 00 ?? ?? ?? ?? 89 51 ??",
            "48 8B 05 ?? ?? ?? ?? 41 8B 94 00 ?? ?? ?? ?? 89 57 ??",
            "140000000",
            "163B0A000",
            WindowsApi.RegionPageProtection.PAGE_READONLY
            );
    }
}
