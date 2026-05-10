using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace wpf.ui.controls.buttons
{
    public class BreakerStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 ||
                !(values[0] is double width) ||
                !(values[1] is BreakerType type))
            {
                return new CornerRadius(0);
            }

            return type switch
            {
                BreakerType.Isolation => new CornerRadius(width / 2),
                _ => new CornerRadius(0)
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
