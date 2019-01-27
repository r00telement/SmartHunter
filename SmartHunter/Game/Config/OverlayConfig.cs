using SmartHunter.Core.Config;

namespace SmartHunter.Game.Config
{
    public class OverlayConfig
    {
        public float ScaleMin = 0.5f;
        public float ScaleMax = 2f;
        public float ScaleStep = 0.1f;
        public bool HideWhenGameWindowIsInactive = false;

        public TeamWidgetConfig TeamWidget = new TeamWidgetConfig(1540, 715);
        public MonsterWidgetConfig MonsterWidget = new MonsterWidgetConfig(517, 143);
        public WidgetConfig PlayerWidget = new WidgetConfig(435, 803);
    }
}
