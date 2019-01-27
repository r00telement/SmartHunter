using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class MonsterWidgetContext : WidgetContext
    {
        public ObservableCollection<Monster> Monsters { get; private set; }
        public CollectionViewSource MonstersViewSource { get; private set; }

        bool m_ShowHealthBar = true;
        public bool ShowHealthBar
        {
            get { return m_ShowHealthBar; }
            set { SetProperty(ref m_ShowHealthBar, value); }
        }

        bool m_ShowHealth = true;
        public bool ShowHealth
        {
            get { return m_ShowHealth; }
            set { SetProperty(ref m_ShowHealth, value); }
        }

        bool m_ShowCrown = true;
        public bool ShowCrown
        {
            get { return m_ShowCrown; }
            set { SetProperty(ref m_ShowCrown, value); }
        }

        bool m_ShowParts = true;
        public bool ShowParts
        {
            get { return m_ShowParts; }
            set { SetProperty(ref m_ShowParts, value); }
        }

        bool m_ShowStatusEffects = true;
        public bool ShowStatusEffects
        {
            get { return m_ShowStatusEffects; }
            set { SetProperty(ref m_ShowStatusEffects, value); }
        }

        public MonsterWidgetContext()
        {
            Monsters = new ObservableCollection<Monster>();

            MonstersViewSource = new CollectionViewSource();
            MonstersViewSource.Source = Monsters;
            MonstersViewSource.IsLiveFilteringRequested = true;
            MonstersViewSource.LiveFilteringProperties.Add(nameof(Monster.Id));
            MonstersViewSource.Filter += (sender, e) =>
            {
                var monster = e.Item as Monster;
                e.Accepted = ConfigHelper.Main.Values.Overlay.MonsterWidget.MatchIncludeMonsterIdRegex(monster.Id);
            };

            UpdateFromConfig();
        }

        public Monster UpdateAndGetMonster(ulong address, string id, float maxHealth, float currentHealth, float sizeScale)
        {
            Monster monster = null;

            monster = Monsters.FirstOrDefault(existingMonster => existingMonster.Address == address);
            if (monster != null)
            {
                monster.Id = id;
                monster.Health.Max = maxHealth;
                monster.Health.Current = currentHealth;
                monster.SizeScale = sizeScale;

                return monster;
            }

            monster = new Monster(address, id, maxHealth, currentHealth, sizeScale);
            Monsters.Add(monster);

            return monster;
        }

        public override void UpdateFromConfig()
        {
            base.UpdateFromConfig();

            ShowHealthBar = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowHealthBar;
            ShowHealth = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowHealth;
            ShowCrown = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowCrown;
            ShowParts = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowParts;
            ShowStatusEffects = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowStatusEffects;
        }
    }
}
