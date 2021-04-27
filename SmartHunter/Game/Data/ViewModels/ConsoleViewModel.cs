using SmartHunter.Core;
using SmartHunter.Core.Data;

namespace SmartHunter.Game.Data.ViewModels
{
    public class ConsoleViewModel : Bindable
    {
        static ConsoleViewModel s_Instance = null;
        public static ConsoleViewModel Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new ConsoleViewModel();
                }

                return s_Instance;
            }
        }

        string m_Output;
        public string Output
        {
            get { return m_Output; }
            set { SetProperty(ref m_Output, value); }
        }

        public ConsoleViewModel()
        {
            Log.LineReceived += Log_LineReceived;
        }

        void Log_LineReceived(object sender, GenericEventArgs<string> e)
        {
            Output += $"{e.Value}\r\n";
        }
    }
}
