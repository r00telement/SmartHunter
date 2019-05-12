using System;

namespace SmartHunter.Core.Data
{
    public abstract class TimedVisibility : Bindable
    {
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

        protected bool IsTimeVisible(bool showUnchanged, float hideAfterSeconds)
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
    }
}
