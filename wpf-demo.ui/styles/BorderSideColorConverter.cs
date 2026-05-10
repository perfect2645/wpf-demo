using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace wpf.ui.styles
{
    public class BorderSideColorConverter : IMultiValueConverter
    {
        public string? Top { get; set; }
        public string? Left { get; set; }
        public string? Bottom { get; set; }
        public string? Right { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString(Top), 0),
                    new GradientStop((Color)ColorConverter.ConvertFromString(Right), 1),
                },
                StartPoint = new System.Windows.Point(0, 0),
                EndPoint = new System.Windows.Point(1, 1)
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
