using SmartHunter.Game.Data;
using SmartHunter.Game.Data.ViewModels;
using System.Windows.Data;

namespace SmartHunter.Ui.Converters
{
    public class PlayerToPlayerIndexConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var player = value as Player;

            if (OverlayViewModel.Instance.TeamWidget.Context.Players.Contains(player))
            {
                return OverlayViewModel.Instance.TeamWidget.Context.Players.IndexOf(player);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
