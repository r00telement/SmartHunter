using System.Windows;
using System.Windows.Data;

namespace SmartHunter.Ui.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; } = Visibility.Visible;
        public Visibility FalseValue { get; set; } = Visibility.Collapsed;

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool invert = parameter != null;

            if (value is bool)
            {
                if ((bool)value)
                {
                    return !invert ? TrueValue : FalseValue;
                }
                else
                {
                    return !invert ? FalseValue : TrueValue;
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}