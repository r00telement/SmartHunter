using SmartHunter.Core;

namespace SmartHunter.Game.Config
{
    public class BytePatternConfig
    {
        public string String;
        public string AddressRangeStart = "140000000";
        public string AddressRangeEnd = "145000000";
        public WindowsApi.RegionPageProtection[] PageProtections;

        public BytePatternConfig(string patternString, string addressRangeStart, string addressRangeEnd, params WindowsApi.RegionPageProtection[] pageProtections)
        {
            String = patternString;
            AddressRangeStart = addressRangeStart;
            AddressRangeEnd = addressRangeEnd;
            PageProtections = pageProtections;
        }
    }
}
