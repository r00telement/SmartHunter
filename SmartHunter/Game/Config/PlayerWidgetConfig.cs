using SmartHunter.Core.Config;
using System.Text.RegularExpressions;

namespace SmartHunter.Game.Config
{
    public class PlayerWidgetConfig : WidgetConfig
    {
        public string IncludePlayerStatusEffectGroupIdRegex = ".*";

        public PlayerWidgetConfig(float x, float y) : base(x, y)
        {
        }

        public bool MatchIncludePlayerStatusEffectGroupIdRegex(string groupId)
        {
            return new Regex(IncludePlayerStatusEffectGroupIdRegex).IsMatch(groupId);
        }
    }
}
