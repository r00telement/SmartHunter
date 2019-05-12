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

        public MonsterStatusEffectConfig(string nameStringId, string pointerOffset, string maxDurationOffset, string currentBuildupOffset, string maxBuildupOffset, string currentDurationoffset, string timesActivatedOffset)
        {
            NameStringId = nameStringId;
            PointerOffset = pointerOffset;

            MaxDurationOffset = maxDurationOffset;
            CurrentBuildupOffset = currentBuildupOffset;
            MaxBuildupOffset = maxBuildupOffset;
            CurrentDurationOffset = currentDurationoffset;
            TimesActivatedOffset = timesActivatedOffset;
        }
    }
}