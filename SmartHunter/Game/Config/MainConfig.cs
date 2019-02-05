using SmartHunter.Core.Config;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmartHunter.Game.Config
{
    public class MainConfig
    {
        public string LocalizationFileName = "en-US.json";
        public string SkinFileName = "Default.xaml";
        public string MonsterDataFileName = "MonsterData.json";
        public string PlayerDataFileName = "PlayerData.json";

        public OverlayConfig Overlay = new OverlayConfig();

        [PreserveCollectionIntegrity]
        public Dictionary<InputControl, Key> Keybinds = new Dictionary<InputControl, Key>()
        {
            { InputControl.ManipulateWidget, Key.LeftAlt }
        };

        public DebugConfig Debug = new DebugConfig();
    }
}