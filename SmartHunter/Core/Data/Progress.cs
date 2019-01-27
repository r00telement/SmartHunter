using System;

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
                    NotifyPropertyChanged(nameof(ArcData));
                    NotifyPropertyChanged(nameof(ArcDataRemainder));
                }
            }
        }

        public float Fraction { get { return m_Current / m_Max; } }

        public string ArcData
        {
            get
            {
                return DescribeArc(0, 360 * Fraction);
            }
        }

        public string ArcDataRemainder
        {
            get
            {
                return DescribeArc(360 * Fraction, 360);
            }
        }

        public Progress(float max, float current)
        {
            m_Max = max;
            m_Current = current;
        }

        void PolarToCartesian(float centerX, float centerY, float radius, float degrees, out float x, out float y)
        {
            var radians = (degrees - 90) * Math.PI / 180.0;

            x = centerX + (float)(radius * Math.Cos(radians));
            y = centerY + (float)(radius * Math.Sin(radians));
        }

        string DescribeArc(float startAngle, float endAngle)
        {
            // Won't render if end angle is precisely 360
            if (endAngle == 360)
            {
                endAngle -= 0.0001f;
            }

            float x = 100;
            float y = 100;
            float radius = 100;

            float startX;
            float startY;
            PolarToCartesian(x, y, radius, endAngle, out startX, out startY);

            float endX;
            float endY;
            PolarToCartesian(x, y, radius, startAngle, out endX, out endY);

            var arcSweep = endAngle - startAngle <= 180 ? "0" : "1";
            
            return String.Format("M {0},{1} A {2} {3} 0 {4} 0 {5},{6} L {7},{8} L {9},{10}", startX, startY, radius, radius, arcSweep, endX, endY, x, y, startX, startY);
        }
    }
}
