using System;

namespace SmartHunter.Core.Data
{
    public class Progress : Bindable, IComparable
    {
        float m_Max;
        public float Max
        {
            get { return m_Max; }
            set
            {
                SetProperty(ref m_Max, value);
            }
        }

        float m_Current;
        public float Current
        {
            get { return m_Current; }
            set
            {
                if (ShouldCapCurrent)
                { 
                    value = Cap(value, m_Max);
                }

                if (SetProperty(ref m_Current, value))
                {
                    if (m_Current > m_Max)
                    {
                        Max = m_Current;
                    }

                    NotifyPropertyChanged(nameof(Fraction));
                    NotifyPropertyChanged(nameof(Angle));
                }
            }
        }

        public float Fraction { get { return m_Current / m_Max; } }
        public float Angle { get { return Fraction * 359.999f; } }

        public bool ShouldCapCurrent { get; set; }

        public Progress(float max, float current, bool shouldCapCurrent = false)
        {
            ShouldCapCurrent = shouldCapCurrent;

            Max = max;
            Current = current;
        }

        // We only really want the default compare to compare nulls.
        // We can then compare by Current in a separate pass for better control.
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            return 0;
        }

        public float Cap(float value, float max)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > max)
            {
                value = max;
            }

            return value;
        }
    }
}
