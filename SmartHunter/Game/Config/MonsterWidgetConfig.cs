using SmartHunter.Core.Config;
using System;
using System.Text.RegularExpressions;

namespace SmartHunter.Game.Config
{
    public class MonsterWidgetConfig : WidgetConfig
    {
        // em[0-9]|ems[0-9]|gm[0-9]
        public string IncludeMonsterIdRegex = "em[0-9]";
        public bool ShowUnchangedParts = false;
        public float HidePartsAfterSeconds = 20f;
        public bool ShowUnchangedStatusEffects = false;
        public float HideStatusEffectsAfterSeconds = 20f;

        public MonsterWidgetConfig(float x, float y) : base(x, y)
        {
        }

        public bool MatchIncludeMonsterIdRegex(string monsterId)
        {
            return new Regex(IncludeMonsterIdRegex).IsMatch(monsterId);
        }
    }
}
