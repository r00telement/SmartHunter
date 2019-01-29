using SmartHunter.Core.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class PlayerWidgetContext : WidgetContext
    {
        private class PlayerStatusEffectSorter : IComparer
        {
            private static readonly int s_Before = -1;
            private static readonly int s_After = 1;

            public int Compare(object obj1, object obj2)
            {
                var playerStatusEffect1 = obj1 as PlayerStatusEffect;
                var playerStatusEffect2 = obj2 as PlayerStatusEffect;

                if (playerStatusEffect1.Time == null && playerStatusEffect2.Time == null)
                {
                    return playerStatusEffect1.Name.CompareTo(playerStatusEffect2.Name);
                }
                if (playerStatusEffect1.Time == null)
                {
                    return s_After;
                }
                else if (playerStatusEffect2.Time == null)
                {
                    return s_Before;
                }
                else if (playerStatusEffect1.Time.Current < playerStatusEffect2.Time.Current)
                {
                    return s_Before;
                }
                else if (playerStatusEffect1.Time.Current > playerStatusEffect2.Time.Current)
                {
                    return s_After;
                }
                
                return playerStatusEffect1.Name.CompareTo(playerStatusEffect2.Name);
            }
        }

        public ObservableCollection<PlayerStatusEffect> StatusEffects { get; private set; }
        public CollectionViewSource StatusEffectsViewSource { get; private set; }

        public PlayerWidgetContext()
        {
            StatusEffects = new ObservableCollection<PlayerStatusEffect>();

            StatusEffectsViewSource = new CollectionViewSource();
            StatusEffectsViewSource.Source = StatusEffects;
            StatusEffectsViewSource.IsLiveSortingRequested = true;
            StatusEffectsViewSource.LiveSortingProperties.Add(nameof(PlayerStatusEffect.IsVisible));
            StatusEffectsViewSource.LiveSortingProperties.Add(nameof(PlayerStatusEffect.Time.Current));
            (StatusEffectsViewSource.View as ListCollectionView).CustomSort = new PlayerStatusEffectSorter();
        }

        public PlayerStatusEffect UpdateAndGetPlayerStatusEffect(int index, float? currentTime, bool isActive)
        {
            PlayerStatusEffect playerStatusEffect = StatusEffects.SingleOrDefault(statusEffect => statusEffect.Index == index);
            if (playerStatusEffect == null && isActive)
            {
                playerStatusEffect = new PlayerStatusEffect(index, currentTime, currentTime, isActive);
                StatusEffects.Add(playerStatusEffect);
            }
            else if (playerStatusEffect != null)
            {
                if (currentTime.HasValue)
                {
                    playerStatusEffect.Time.Current = currentTime.Value;
                }

                playerStatusEffect.IsVisible = isActive;
            }

            return playerStatusEffect;
        }
    }
}
