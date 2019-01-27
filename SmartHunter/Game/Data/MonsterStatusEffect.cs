using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;
using System;

namespace SmartHunter.Game.Data
{
    public class MonsterStatusEffect : Bindable
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

        bool m_IsVisible = false;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { SetProperty(ref m_IsVisible, value); }
        }

        public DateTimeOffset InitialTime { get; private set; }
        public DateTimeOffset? LastChangedTime { get; private set; }

        public MonsterStatusEffect(Monster owner, ulong address, int id, float maxBuildup, float currentBuildup, float maxDuration, float currentDuration, int timesActivatedCount)
        {
            m_Owner = owner;
            Address = address;
            Id = id;
            Buildup = new Progress(maxBuildup, currentBuildup);
            Duration = new Progress(maxDuration, maxDuration - currentDuration);
            m_TimesActivatedCount = timesActivatedCount;

            InitialTime = DateTimeOffset.UtcNow;

            PropertyChanged += MonsterStatusEffect_PropertyChanged;
            Buildup.PropertyChanged += Bar_PropertyChanged;
            Duration.PropertyChanged += Bar_PropertyChanged;
        }

        private void MonsterStatusEffect_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimesActivatedCount))
            {
                LastChangedTime = DateTimeOffset.UtcNow;
            }
        }

        private void Bar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LastChangedTime = DateTimeOffset.UtcNow;
        }
    }
}
