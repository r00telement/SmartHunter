using SmartHunter.Core.Config;
using System.Text.RegularExpressions;

namespace SmartHunter.Game.Config
{
    public class PlayerWidgetConfig : WidgetConfig
    {
        public string StatusEffectGroupIdRegex = "";

        public PlayerWidgetConfig(float x, float y) : base(x, y)
        {
        }

        public bool MatchStatusEffectGroupId(string groupId)
        {
            return new Regex(StatusEffectGroupIdRegex).IsMatch(groupId);
        }
    }
}
