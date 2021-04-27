using SmartHunter.Game.Data;

namespace SmartHunter.Game.Config
{
    public class StatusEffectConfig
    {
        public enum MemorySource : uint
        {
            Base = 1,
            Equipment = 2
        }

        public string GroupId;
        public string NameStringId;
        public string TimerOffset;
        public uint Source;
        public MemoryConditionConfig[] Conditions;

        public StatusEffectConfig(string groupId, string nameStringId, uint source, string timerOffset, params MemoryConditionConfig[] conditions)
        {
            GroupId = groupId;
            NameStringId = nameStringId;
            Source = source;
            TimerOffset = timerOffset;
            Conditions = conditions;
        }
    }
}
