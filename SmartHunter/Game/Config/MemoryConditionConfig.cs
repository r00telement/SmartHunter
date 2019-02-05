namespace SmartHunter.Game.Config
{
    public class MemoryConditionConfig
    {
        public string[] Offsets;
        public byte? ByteValue;
        public int? IntValue;
        public string StringRegexValue;

        public MemoryConditionConfig()
        {
        }

        public MemoryConditionConfig(byte byteValue, params string[] offsets)
        {
            Offsets = offsets;
            ByteValue = byteValue;
        }

        public MemoryConditionConfig(int intValue, params string[] offsets)
        {
            Offsets = offsets;
            IntValue = intValue;
        }

        public MemoryConditionConfig(string stringRegexValue, params string[] offsets)
        {
            Offsets = offsets;
            StringRegexValue = stringRegexValue;
        }
    }
}
