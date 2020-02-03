using SmartHunter.Core.Config;

namespace SmartHunter.Game.Config
{
    public class TeamWidgetConfig : WidgetConfig
    {
        public bool ShowBars = true;
        public bool ShowNumbers = true;
        public bool ShowPercents = true;
        public bool ShowDPS = true;
        public int DPSTimeWindowInSeconds = 60;

        public TeamWidgetConfig(float x, float y) : base(x, y)
        {
        }
    }
}
