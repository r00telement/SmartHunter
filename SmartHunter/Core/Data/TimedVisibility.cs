using System;

namespace SmartHunter.Core.Data
{
    public abstract class TimedVisibility : Bindable
    {
        bool m_IsVisible = false;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { SetProperty(ref m_IsVisible, value); }
        }

        public DateTimeOffset InitialTime { get; private set; }
        public DateTimeOffset? LastChangedTime { get; private set; }

        public event EventHandler<GenericEventArgs<DateTimeOffset>> Changed;

        public TimedVisibility()
        {
            InitialTime = DateTimeOffset.UtcNow;
        }

        protected void UpdateLastChangedTime()
        {
            LastChangedTime = DateTimeOffset.UtcNow;

            if (Changed != null)
            {
                Changed(this, new GenericEventArgs<DateTimeOffset>(LastChangedTime.Value));
            }
        }

        protected bool CanBeVisible(bool showUnchanged, float hideAfterSeconds)
        {
            if (!showUnchanged && LastChangedTime == null)
            {
                return false;
            }

            DateTimeOffset time = InitialTime;
            if (LastChangedTime != null)
            {
                time = LastChangedTime.Value;
            }

            var hideTime = time.AddSeconds(hideAfterSeconds);
            if (hideTime < DateTimeOffset.UtcNow)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public abstract void UpdateVisibility();
    }
}
