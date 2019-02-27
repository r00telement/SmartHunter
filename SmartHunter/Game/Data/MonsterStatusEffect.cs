using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data
{
    public class MonsterStatusEffect : TimedVisibility
    {
        Monster m_Owner;

        public ulong Address { get; private set; }

        int m_Id;
        public int Id
        {
            get { return m_Id; }
            set
            {
                if (SetProperty(ref m_Id, value))
                {
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

        public Progress Buildup { get; private set; }
        public Progress Duration { get; private set; }

        int m_TimesActivatedCount;
        public int TimesActivatedCount
        {
            get { return m_TimesActivatedCount; }
            set { SetProperty(ref m_TimesActivatedCount, value); }
        }

        public string Name
        {
            get
            {
                return LocalizationHelper.GetMonsterStatusEffectName(Id);
            }
        }

        public MonsterStatusEffect(Monster owner, ulong address, int id, float maxBuildup, float currentBuildup, float maxDuration, float currentDuration, int timesActivatedCount)
        {
            m_Owner = owner;
            Address = address;
            Id = id;
            Buildup = new Progress(maxBuildup, currentBuildup);
            Duration = new Progress(maxDuration, maxDuration - currentDuration);
            m_TimesActivatedCount = timesActivatedCount;

            PropertyChanged += MonsterStatusEffect_PropertyChanged;
            Buildup.PropertyChanged += Bar_PropertyChanged;
            Duration.PropertyChanged += Bar_PropertyChanged;
        }

        private void MonsterStatusEffect_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimesActivatedCount))
            {
                UpdateLastChangedTime();
            }
        }

        private void Bar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateLastChangedTime();
        }

        public override void UpdateVisibility()
        {
            IsVisible = CanBeVisible(ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowUnchangedStatusEffects, ConfigHelper.Main.Values.Overlay.MonsterWidget.HideStatusEffectsAfterSeconds);
        }
    }
}
