using System.Collections.ObjectModel;
using System.Linq;
using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class MonsterWidgetContext : WidgetContext
    {
        public ObservableCollection<Monster> Monsters { get; private set; }

        bool m_ShowCrown = true;
        public bool ShowCrown
        {
            get { return m_ShowCrown; }
            set { SetProperty(ref m_ShowCrown, value); }
        }

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

        bool m_ShowPercents = true;
        public bool ShowPercents
        {
            get { return m_ShowPercents; }
            set { SetProperty(ref m_ShowPercents, value); }
        }

        public MonsterWidgetContext()
        {
            Monsters = new ObservableCollection<Monster>();

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
            }
            else
            {
                monster = new Monster(address, id, maxHealth, currentHealth, sizeScale);
                Monsters.Add(monster);
            }

            monster.NotifyPropertyChanged(nameof(Monster.IsVisible));

            return monster;
        }

        public override void UpdateFromConfig()
        {
            base.UpdateFromConfig();

            ShowCrown = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowCrown;
            ShowBars = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowBars;
            ShowNumbers = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowNumbers;
            ShowPercents = ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowPercents;

            foreach (var monster in Monsters)
            {
                monster.NotifyPropertyChanged(nameof(Monster.IsVisible));
            }
        }
    }
}
