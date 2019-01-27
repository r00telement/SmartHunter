using SmartHunter.Core.Config;

namespace SmartHunter.Core.Data
{
    public class ContextualWidget<TWidgetContext> : Widget where TWidgetContext : WidgetContext
    {
        private TWidgetContext m_Context;
        public TWidgetContext Context
        {
            get { return m_Context; }
            set { SetProperty(ref m_Context, value); }
        }

        public ContextualWidget(WidgetConfig widgetConfig, TWidgetContext context = null) : base(widgetConfig)
        {
            m_Context = context;
        }

        public override void UpdateFromConfig()
        {
            base.UpdateFromConfig();

            if (Context != null)
            {
                Context.UpdateFromConfig();
            }
        }
    }
}
