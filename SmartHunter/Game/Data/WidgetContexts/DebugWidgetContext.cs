using System.Collections.ObjectModel;
using System.Linq;
using SmartHunter.Core.Data;
using SmartHunter.Game.Data;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class DebugWidgetContext : WidgetContext
    {
        public Game CurrentGame { get; set; }
        public string TEST { get { return "Peppaaaaaaaaaaaaaaaaa"; } }
        
        public void UpdateCurrentGame(string playerName, string weaponString, string sessionID, string sessionHostName, string lobbyID, string lobbyHostName)
        {
            CurrentGame.CurrentPlayerName = playerName;
            CurrentGame.CurrentWeaponString = weaponString;
            CurrentGame.SessionID = sessionID;
            CurrentGame.SessionHostPlayerName = sessionHostName;
            CurrentGame.LobbyID = lobbyID;
            CurrentGame.LobbyHostPlayerName = lobbyHostName;
        }
        public DebugWidgetContext()
        {
            CurrentGame = new Game();
        }

        public override void UpdateFromConfig()
        {
            base.UpdateFromConfig();
        }
    }
}
