using SmartHunter.Game.Data.ViewModels;
using System.Windows;

namespace SmartHunter.Ui.Windows
{
    public partial class ConsoleWindow : Window
    {
        public ConsoleWindow()
        {
            InitializeComponent();

            DataContext = ConsoleViewModel.Instance;
        }
    }
}
