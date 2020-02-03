using System.Linq;
using System.Windows.Data;

namespace SmartHunter.Ui.Converters
{
    public class StringArrayContainsConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string[])
            {
                var stringArray = (string[])value;
                return stringArray.Contains(parameter as string);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
