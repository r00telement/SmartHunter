using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data
{
    public class PlayerStatusEffect : Bindable
    {
        int m_Index;
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (SetProperty(ref m_Index, value))
                {
                    NotifyPropertyChanged(nameof(GroupId));
                    UpdateLocalization();
                }
            }
        }

        public string GroupId
        {
            get
            {
                return ConfigHelper.PlayerData.Values.PlayerStatusEffects[Index].GroupId;
            }
        }

        public string Name
        {
            get
            {
                return LocalizationHelper.GetPlayerStatusEffectName(Index);
            }
        }

        public Progress Time { get; private set; }

        bool m_IsVisible;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { SetProperty(ref m_IsVisible, value); }
        }

        public PlayerStatusEffect(int index, float? maxTime, float? currentTime, bool isVisible)
        {
            m_Index = index;

            if (maxTime.HasValue && currentTime.HasValue)
            {
                Time = new Progress(maxTime.Value, currentTime.Value);
            }

            m_IsVisible = isVisible;
        }

        public void UpdateLocalization()
        {
            NotifyPropertyChanged(nameof(Name));
        }
    }
}
