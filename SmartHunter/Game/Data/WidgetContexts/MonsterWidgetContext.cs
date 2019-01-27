using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class MonsterWidgetContext : Bindable
    {
        public ObservableCollection<Monster> Monsters { get; private set; }
        public CollectionViewSource MonstersViewSource { get; private set; }
        
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
    }
}
