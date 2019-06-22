using SmartHunter.Core.Config;
using System;
using System.Text.RegularExpressions;

namespace SmartHunter.Game.Config
{
    public class PlayerWidgetConfig : WidgetConfig
    {
        public string StatusEffectTagsRegex = ".*|Buff|Debuff|HuntingHorn|CoralOrchestra|Equipment|Weapon";

        public PlayerWidgetConfig(float x, float y) : base(x, y)
        {
        }

        public bool MatchStatusEffectTags(string[] tags)
        {
            return new Regex(StatusEffectTagsRegex, RegexOptions.CultureInvariant).IsMatch(String.Join(" ", tags));
        }
    }
}
