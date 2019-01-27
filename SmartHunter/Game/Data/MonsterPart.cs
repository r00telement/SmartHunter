using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;
using System;

namespace SmartHunter.Game.Data
{
    public class MonsterPart : Bindable
    {
        Monster m_Owner;
        public ulong Address { get; private set; }

        bool m_IsRemovable;
        public bool IsRemovable
        {
            get { return m_IsRemovable; }
            set { SetProperty(ref m_IsRemovable, value); }
        }

        public Progress Health { get; private set; }

        int m_TimesBrokenCount;
        public int TimesBrokenCount
        {
            get { return m_TimesBrokenCount; }
            set { SetProperty(ref m_TimesBrokenCount, value); }
        }

        public string Name
        {
            get
            {
                if (IsRemovable)
                {
                    return LocalizationHelper.GetMonsterRemovablePartName(m_Owner.Id, m_Owner.RemovableParts.IndexOf(this));
                }

                return LocalizationHelper.GetMonsterPartName(m_Owner.Id, m_Owner.Parts.IndexOf(this));
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

        public MonsterPart(Monster owner, ulong address, bool isRemovable, float maxHealth, float currentHealth, int timesBrokenCount)
        {
            m_Owner = owner;
            Address = address;
            m_IsRemovable = isRemovable;
            Health = new Progress(maxHealth, currentHealth);
            m_TimesBrokenCount = timesBrokenCount;

            InitialTime = DateTimeOffset.UtcNow;

            PropertyChanged += MonsterPart_PropertyChanged;
            Health.PropertyChanged += Health_PropertyChanged;
        }

        private void MonsterPart_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimesBrokenCount))
            {
                LastChangedTime = DateTimeOffset.UtcNow;
            }
        }

        private void Health_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Progress.Current))
            {
                LastChangedTime = DateTimeOffset.UtcNow;
            }
        }
    }
}
