using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class TeamWidgetContext : WidgetContext
    {
        public ObservableCollection<Player> Players { get; private set; }

        bool m_ShowBars = true;
        public bool ShowBars
        {
            get { return m_ShowBars; }
            set { SetProperty(ref m_ShowBars, value); }
        }

        bool m_ShowNumbers = true;
        public bool ShowNumbers
        {
            get { return m_ShowNumbers; }
            set { SetProperty(ref m_ShowNumbers, value); }
        }

        bool m_ShowPercents = false;
        public bool ShowPercents
        {
            get { return m_ShowPercents; }
            set { SetProperty(ref m_ShowPercents, value); }
        }

        public TeamWidgetContext()
        {
            Players = new ObservableCollection<Player>();

            UpdateFromConfig();
        }

        public Player UpdateAndGetPlayer(int index, string name, int damage)
        {
            if (String.IsNullOrEmpty(name) && damage == 0)
            {
                return null;
            }
            
            while (index >= Players.Count)
            {
                Players.Add(new Player() { Index = Players.Count, Name = LocalizationHelper.GetString(LocalizationHelper.UnknownPlayerStringId) });
            }

            Player player = Players[index];
            if (!String.IsNullOrEmpty(name))
            {
                player.Name = name;
            }
            else if (String.IsNullOrEmpty(player.Name))
            {
                player.Name = LocalizationHelper.GetString(LocalizationHelper.UnknownPlayerStringId);
            }

            player.Damage = damage;

            return player;
        }

        public void UpdateFractions()
        {
            var playersWithDamage = Players.Where(player => player.Damage > 0);
            if (!playersWithDamage.Any())
            {
                foreach (var player in Players)
                {
                    player.DamageFraction = 0;
                    player.BarFraction = 0;
                }

                return;
            }

            var highestDamagePlayers = Players.OrderByDescending(player => player.Damage).Take(1);
            if (highestDamagePlayers.Any())
            {
                int totalDamage = Players.Sum(player => player.Damage);

                var highestDamagePlayer = highestDamagePlayers.First();
                highestDamagePlayer.DamageFraction = (float)highestDamagePlayer.Damage / (float)totalDamage;
                highestDamagePlayer.BarFraction = 1;

                foreach (var otherPlayer in Players.Except(highestDamagePlayers))
                {
                    otherPlayer.DamageFraction = (float)otherPlayer.Damage / (float)totalDamage;
                    otherPlayer.BarFraction = (float)otherPlayer.Damage / (float)highestDamagePlayer.Damage;
                }
            }
        }

        public override void UpdateFromConfig()
        {
            base.UpdateFromConfig();

            ShowBars = ConfigHelper.Main.Values.Overlay.TeamWidget.ShowBars;
            ShowNumbers = ConfigHelper.Main.Values.Overlay.TeamWidget.ShowNumbers;
            ShowPercents = ConfigHelper.Main.Values.Overlay.TeamWidget.ShowPercents;
        }
    }
}
