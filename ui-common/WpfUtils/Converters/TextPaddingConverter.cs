using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfUtils.Converters
{
    public class TextPaddingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3 ||
                !(values[0] is string stationName) ||
                !(values[1] is double fontSize) ||
                !(values[2] is double addingOffpset))
            {
                return new Thickness(0);
            }

            int nameLength = stationName.Length;
            if (nameLength != 2) // 仅处理2字场景
            {
                return new Thickness(0);
            }

            return new Thickness(fontSize / 2, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
