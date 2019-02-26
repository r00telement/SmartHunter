using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data
{
    public class MonsterPart : ChangeableVisibility
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
        
        public MonsterPart(Monster owner, ulong address, bool isRemovable, float maxHealth, float currentHealth, int timesBrokenCount)
        {
            m_Owner = owner;
            Address = address;
            m_IsRemovable = isRemovable;
            Health = new Progress(maxHealth, currentHealth);
            m_TimesBrokenCount = timesBrokenCount;

            PropertyChanged += MonsterPart_PropertyChanged;
            Health.PropertyChanged += Health_PropertyChanged;
        }

        private void MonsterPart_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimesBrokenCount))
            {
                UpdateLastChangedTime();
            }
        }

        private void Health_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Progress.Current))
            {
                UpdateLastChangedTime();
            }
        }

        public override void UpdateVisibility()
        {
            IsVisible = CanShow(InitialTime, LastChangedTime, ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowUnchangedParts, ConfigHelper.Main.Values.Overlay.MonsterWidget.HidePartsAfterSeconds);
        }
    }
}
