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
                    NotifyPropertyChanged(nameof(Tags));
                    NotifyPropertyChanged(nameof(Name));
                    NotifyPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public Progress Time { get; private set; }

        bool m_IsConditionPassed;
        public bool IsConditionPassed
        {
            get { return m_IsConditionPassed; }
            set
            {
                if (SetProperty(ref m_IsConditionPassed, value))
                {
                    NotifyPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public string Name
        {
            get
            {
                return LocalizationHelper.GetPlayerStatusEffectName(Index);
            }
        }

        public string[] Tags
        {
            get
            {
                return GetTagsFromIndex(Index);
            }
        }

        public bool IsVisible
        {
            get
            {
                return IsIncluded(Tags) && IsConditionPassed;
            }
        }

        public PlayerStatusEffect(int index, float? maxTime, float? currentTime, bool isConditionPassed)
        {
            m_Index = index;

            if (maxTime.HasValue && currentTime.HasValue)
            {
                Time = new Progress(maxTime.Value, currentTime.Value);
            }

            m_IsConditionPassed = isConditionPassed;
        }

        public static string[] GetTagsFromIndex(int index)
        {
            return ConfigHelper.PlayerData.Values.StatusEffects[index].Tags;
        }

        public static bool IsIncluded(string[] tags)
        {
            return ConfigHelper.Main.Values.Overlay.PlayerWidget.MatchStatusEffectTags(tags);
        }
    }
}
