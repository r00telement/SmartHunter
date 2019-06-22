using System.Collections.Generic;
using System.Linq;

namespace SmartHunter.Game.Config
{
    public class StatusEffectConfig
    {
        public enum MemorySource
        {
            Base,
            Equipment,
            Weapon
        }

        public string NameStringId;
        public string[] Tags;
        public string TimerOffset;
        public MemorySource Source;
        public MemoryConditionConfig[] Conditions;

        public StatusEffectConfig(string nameStringId, IEnumerable<string> tags, MemorySource source, string timerOffset, params MemoryConditionConfig[] conditions)
        {
            NameStringId = nameStringId;
            Tags = tags.ToArray();
            Source = source;
            TimerOffset = timerOffset;
            Conditions = conditions;
        }
    }
}
