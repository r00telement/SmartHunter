using SmartHunter.Core.Config;

namespace SmartHunter.Core.Data
{
    public class Widget : Bindable
    {
        WidgetConfig m_WidgetConfig;

        float m_X;
        public float X
        {
            get { return m_X; }
            set
            {
                if (SetProperty(ref m_X, value) && m_X != m_WidgetConfig.X)
                {
                    UpdateConfig();
                }
            }
        }

        float m_Y;
        public float Y
        {
            get { return m_Y; }
            set
            {
                if (SetProperty(ref m_Y, value) && m_Y != m_WidgetConfig.Y)
                {
                    UpdateConfig();
                }
            }
        }

        float m_Scale = 1;
        public float Scale
        {
            get { return m_Scale; }
            set
            {
                if (SetProperty(ref m_Scale, value) && m_Scale != m_WidgetConfig.Scale)
                {
                    UpdateConfig();
                }
            }
        }

        bool m_IsVisible = true;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set
            {
                if (SetProperty(ref m_IsVisible, value) && m_IsVisible != m_WidgetConfig.IsVisible)
                {
                    UpdateConfig();
                }
            }
        }

        public bool CanSaveConfig { get; set; }

        public Widget(WidgetConfig widgetConfig)
        {
            m_WidgetConfig = widgetConfig;
            UpdateFromConfig();            
        }

        void UpdateConfig()
        {
            m_WidgetConfig.X = m_X;
            m_WidgetConfig.Y = m_Y;
            m_WidgetConfig.Scale = m_Scale;
            m_WidgetConfig.IsVisible = m_IsVisible;

            CanSaveConfig = true;
        }

        public void UpdateFromConfig()
        {
            X = m_WidgetConfig.X;
            Y = m_WidgetConfig.Y;
            Scale = m_WidgetConfig.Scale;
            IsVisible = m_WidgetConfig.IsVisible;
        }
    }
}
