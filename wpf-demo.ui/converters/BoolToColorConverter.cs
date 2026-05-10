using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace wpf.ui.converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b)
                ? new SolidColorBrush(Color.FromRgb(255, 88, 0))
                : new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
