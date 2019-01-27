// Monster Size Differences
// https://docs.google.com/spreadsheets/d/1zoRCvF4qKUZCgMGQw3vdtjVS-laX7-px6k1oPCJMg5E

// Monster Crown Sizes
// https://docs.google.com/spreadsheets/d/1w4puqT7mlzG-Zfh1Ztgk0a35vmba-h-HW2o1G00qFA8

namespace SmartHunter.Game.Config
{
    public class MonsterConfig
    {
        public string NameStringId;
        public string[] RemovablePartStringIds;
        public string[] PartStringIds;
        public float BaseSize;
        public float ScaleModifier = 1f;
        public MonsterCrownConfig Crowns;

        public MonsterConfig(string nameStringId, string[] removablePartStringIds, string[] partStringIds, float baseSize, float scaleModifier, MonsterCrownConfig crowns)
        {
            NameStringId = nameStringId;
            RemovablePartStringIds = removablePartStringIds;
            PartStringIds = partStringIds;
            BaseSize = baseSize;
            ScaleModifier = scaleModifier;
            Crowns = crowns;
        }
    }
}
