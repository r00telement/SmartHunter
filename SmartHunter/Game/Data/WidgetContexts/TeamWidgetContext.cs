using SmartHunter.Core.Data;
using SmartHunter.Game.Data.ViewModels;
using SmartHunter.Game.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class TeamWidgetContext : WidgetContext
    {
        private class PlayerSorter : IComparer
        {
            private static readonly int s_Before = -1;
            private static readonly int s_After = 1;
            private static readonly int s_NoChange = 0;

            public int Compare(object obj1, object obj2)
            {
                var player1 = obj1 as Player;
                var player2 = obj2 as Player;

                if (player1.Damage > player2.Damage)
                {
                    return s_Before;
                }
                else if (player1.Damage < player2.Damage)
                {
                    return s_After;
                }

                var player1Index = OverlayViewModel.Instance.TeamWidget.Context.Players.IndexOf(player1);
                var player2Index = OverlayViewModel.Instance.TeamWidget.Context.Players.IndexOf(player2);

                if (player1Index > player2Index)
                {
                    return s_After;
                }
                else if (player1Index < player2Index)
                {
                    return s_Before;
                }

                return s_NoChange;
            }
        }

        public ObservableCollection<Player> Players { get; private set; }
        public CollectionViewSource PlayersViewSource { get; private set; }

        bool m_ShowDamageNumber = true;
        public bool ShowDamageNumber
        {
            get { return m_ShowDamageNumber; }
            set { SetProperty(ref m_ShowDamageNumber, value); }
        }

        public TeamWidgetContext()
        {
            Players = new ObservableCollection<Player>();

            PlayersViewSource = new CollectionViewSource();
            PlayersViewSource.Source = Players;
            PlayersViewSource.IsLiveSortingRequested = true;
            PlayersViewSource.LiveSortingProperties.Add(nameof(Player.Damage));
            (PlayersViewSource.View as ListCollectionView).CustomSort = new PlayerSorter();

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
                Players.Add(new Player() { Name = LocalizationHelper.GetString(LocalizationHelper.UnknownPlayerStringId) });
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

            ShowDamageNumber = ConfigHelper.Main.Values.Overlay.TeamWidget.ShowDamageNumber;
        }
    }
}
