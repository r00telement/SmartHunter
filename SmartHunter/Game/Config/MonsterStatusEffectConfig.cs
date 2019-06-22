using System.Collections.Generic;
using System.Linq;

namespace SmartHunter.Game.Config
{
    public class MonsterStatusEffectConfig
    {
        public string NameStringId;

        public string PointerOffset;

        public string MaxDurationOffset;
        public string CurrentBuildupOffset;
        public string MaxBuildupOffset;
        public string CurrentDurationOffset;
        public string TimesActivatedOffset;

        public bool InvertBuildup;
        public bool InvertDuration;

        public string[] Tags;

        public MonsterStatusEffectConfig(string nameStringId, string pointerOffset, string maxDurationOffset, string currentBuildupOffset, string maxBuildupOffset, string currentDurationoffset, string timesActivatedOffset, bool invertBuildup, bool invertDuration, IEnumerable<string> tags)
        {
            NameStringId = nameStringId;
            PointerOffset = pointerOffset;

            MaxDurationOffset = maxDurationOffset;
            CurrentBuildupOffset = currentBuildupOffset;
            MaxBuildupOffset = maxBuildupOffset;
            CurrentDurationOffset = currentDurationoffset;
            TimesActivatedOffset = timesActivatedOffset;

            InvertBuildup = invertBuildup;
            InvertDuration = invertDuration;

            Tags = tags.ToArray();
        }
    }
}