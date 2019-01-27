using SmartHunter.Core.Config;

namespace SmartHunter.Game.Config
{
    public class TeamWidgetConfig : WidgetConfig
    {
        public bool ShowDamageNumber = true;

        public TeamWidgetConfig(float x, float y) : base(x, y)
        {
        }
    }
}
