using System.Collections.Generic;
using System.Linq;

namespace SmartHunter.Game.Config
{
    public class MonsterStatusEffectConfig
    {
        public string NameStringId;
        public string[] Tags;
        public string PointerOffset;
        public string MaxDurationOffset;
        public string CurrentBuildupOffset;
        public string MaxBuildupOffset;
        public string CurrentDurationOffset;
        public string TimesActivatedOffset;
        public bool InvertBuildup;
        public bool InvertDuration;

        public MonsterStatusEffectConfig(string nameStringId, IEnumerable<string> tags, string pointerOffset, string maxDurationOffset, string currentBuildupOffset, string maxBuildupOffset, string currentDurationoffset, string timesActivatedOffset, bool invertBuildup = false, bool invertDuration = true)
        {
            NameStringId = nameStringId;
            Tags = tags.ToArray();
            PointerOffset = pointerOffset;
            MaxDurationOffset = maxDurationOffset;
            CurrentBuildupOffset = currentBuildupOffset;
            MaxBuildupOffset = maxBuildupOffset;
            CurrentDurationOffset = currentDurationoffset;
            TimesActivatedOffset = timesActivatedOffset;
            InvertBuildup = invertBuildup;
            InvertDuration = invertDuration;
        }
    }
}