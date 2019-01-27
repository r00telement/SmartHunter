using SmartHunter.Core.Config;

namespace SmartHunter.Core.Data
{
    public class ContextualWidget<T> : Widget where T : Bindable
    {
        private T m_Context;
        public T Context
        {
            get { return m_Context; }
            set { SetProperty(ref m_Context, value); }
        }

        public ContextualWidget(WidgetConfig widgetConfig, T context = null) : base(widgetConfig)
        {
            m_Context = context;
        }
    }
}
