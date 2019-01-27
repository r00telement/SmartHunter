namespace SmartHunter.Game.Config
{
    public class PlayerStatusEffectConfig
    {
        public enum MemorySource
        {
            StatusEffect,
            Equipment
        }

        public string GroupId;
        public string NameStringId;
        public string TimerOffset;
        public PlayerStatusEffectConditionConfig Condition;
        public MemorySource Source;

        public PlayerStatusEffectConfig(string groupId, string nameStringId, string timerOffset, PlayerStatusEffectConditionConfig condition = null, MemorySource source = MemorySource.StatusEffect)
        {
            GroupId = groupId;
            NameStringId = nameStringId;
            TimerOffset = timerOffset;
            Condition = condition;
            Source = source;
        }
    }
}
