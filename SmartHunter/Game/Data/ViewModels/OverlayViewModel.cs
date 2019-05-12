using SmartHunter.Core.Data;
using SmartHunter.Game.Data.WidgetContexts;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data.ViewModels
{
    public class OverlayViewModel : Bindable
    {
        static OverlayViewModel s_Instance = null;
        public static OverlayViewModel Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new OverlayViewModel();

                    bool isDesignInstance = System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime;
                    if (isDesignInstance || ConfigHelper.Main.Values.Debug.UseSampleData)
                    {
                        s_Instance.GenerateSampleData();
                    }
                }

                return s_Instance;
            }
        }

        public ContextualWidget<TeamWidgetContext> TeamWidget { get; private set; }
        public ContextualWidget<MonsterWidgetContext> MonsterWidget { get; private set; }
        public ContextualWidget<PlayerWidgetContext> PlayerWidget { get; private set; }

        bool m_CanManipulateWindows;
        public bool CanManipulateWindows
        {
            get { return m_CanManipulateWindows; }
            set { SetProperty(ref m_CanManipulateWindows, value); }
        }

        bool m_IsVisible = true;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { SetProperty(ref m_IsVisible, value); }
        }

        public OverlayViewModel()
        {
            TeamWidget = new ContextualWidget<TeamWidgetContext>(ConfigHelper.Main.Values.Overlay.TeamWidget, new TeamWidgetContext());
            MonsterWidget = new ContextualWidget<MonsterWidgetContext>(ConfigHelper.Main.Values.Overlay.MonsterWidget, new MonsterWidgetContext());
            PlayerWidget = new ContextualWidget<PlayerWidgetContext>(ConfigHelper.Main.Values.Overlay.PlayerWidget, new PlayerWidgetContext());
        }

        void GenerateSampleData()
        {
            TeamWidget.Context.UpdateAndGetPlayer(0, "Jade", 3244);
            TeamWidget.Context.UpdateAndGetPlayer(1, "Kabuto", 2182);
            TeamWidget.Context.UpdateAndGetPlayer(2, "mike the father", 569);
            TeamWidget.Context.UpdateAndGetPlayer(3, "SlashMaster", 2966);

            TeamWidget.Context.UpdateFractions();

            var monster1 = MonsterWidget.Context.UpdateAndGetMonster(0, "em113_00", 6150, 6150, 1.25f);
            MonsterWidget.Context.UpdateAndGetMonster(0, "em113_00", 6150, 2733, 1.45f);
            MonsterWidget.Context.UpdateAndGetMonster(1, "em114_00", 5700, 5700, 0.45f);
            MonsterWidget.Context.UpdateAndGetMonster(2, "em118_00", 9191, 9191, 1.35f);

            monster1.UpdateAndGetPart(0, false, 480, 480, 3);
            monster1.UpdateAndGetPart(0, false, 480, 109, 3);

            monster1.UpdateAndGetPart(1, false, 100, 100, 2);

            monster1.UpdateAndGetPart(2, false, 384, 384, 1);
            monster1.UpdateAndGetPart(2, false, 384, 322, 1);

            monster1.UpdateAndGetPart(3, false, 432, 432, 0);
            monster1.UpdateAndGetPart(3, false, 432, 419, 0);

            monster1.UpdateAndGetPart(4, false, 150, 150, 1);
            monster1.UpdateAndGetPart(4, false, 150, 100, 1);

            monster1.UpdateAndGetPart(4, false, 432, 432, 1);
            monster1.UpdateAndGetPart(4, false, 432, 403, 1);

            monster1.UpdateAndGetPart(0, true, 864, 864, 0);
            monster1.UpdateAndGetPart(0, true, 864, 811, 0);

            monster1.UpdateAndGetStatusEffect(5, 50, 50, 50, 0, 1);
            monster1.UpdateAndGetStatusEffect(5, 50, 40, 50, 0, 1);

            monster1.UpdateAndGetStatusEffect(2, 100, 100, 7, 0, 0);
            monster1.UpdateAndGetStatusEffect(2, 100, 0, 7, 3, 0);

            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(1, 20, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(1, 15.5f, true);

            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(45, 120, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(45, 33.43f, true);

            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(80, null, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(82, null, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(83, 40, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(83, 30, true);

            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(69, 20, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(69, 11.98f, true);

            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(90, 120, true);
            PlayerWidget.Context.UpdateAndGetPlayerStatusEffect(90, 111.5f, true);
        }
    }    
}