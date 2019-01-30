namespace SmartHunter.Core.Data
{
    public class Progress : Bindable
    {
        float m_Max;
        public float Max
        {
            get { return m_Max; }
            set { SetProperty(ref m_Max, value); }
        }

        float m_Current;
        public float Current
        {
            get { return m_Current; }
            set
            {
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

        public Progress(float max, float current)
        {
            m_Max = max;
            m_Current = current;
        }
    }
}
