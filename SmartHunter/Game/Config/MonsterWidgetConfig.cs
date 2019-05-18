using SmartHunter.Core.Config;
using System.Text.RegularExpressions;

namespace SmartHunter.Game.Config
{
    public class MonsterWidgetConfig : WidgetConfig
    {
        // em[0-9]|ems[0-9]|gm[0-9]
        public string MonsterIdInclude = "em[0-9]";
        public string PartGroupIdInclude = "";
        public string StatusEffectGroupIdInclude = "";
        public bool ShowUnchangedMonsters = true;
        public float HideMonstersAfterSeconds = 9999;
        public bool ShowUnchangedParts = false;
        public float HidePartsAfterSeconds = 12f;
        public bool ShowUnchangedStatusEffects = false;
        public float HideStatusEffectsAfterSeconds = 12f;

        public bool ShowCrown = true;
        public bool ShowBars = true;
        public bool ShowNumbers = true;
        public bool ShowPercents = false;

        public MonsterWidgetConfig(float x, float y) : base(x, y)
        {
        }

        public bool MatchMonsterIdInclude(string monsterId)
        {
            return new Regex(MonsterIdInclude).IsMatch(monsterId);
        }

        public bool MatchPartGroupIdInclude(string groupId)
        {
            return new Regex(PartGroupIdInclude).IsMatch(groupId);
        }

        public bool MatchStatusEffectGroupIdInclude(string groupId)
        {
            return new Regex(StatusEffectGroupIdInclude).IsMatch(groupId);
        }
    }
}
