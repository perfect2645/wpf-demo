using System.Windows;
using System.Windows.Controls;
using WpfUtils.Cache;

namespace wpf.ui.views.diagrams.electric
{
    /// <summary>
    /// FullLine.xaml 的交互逻辑
    /// </summary>
    public partial class FullLine : UserControl
    {
        public FullLine()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FullLineDiagram1.Source = ImageCache.Get("电力-全线接线图^1");
            FullLineDiagram2.Source = ImageCache.Get("电力-全线接线图^2");
            FullLineDiagram3.Source = ImageCache.Get("电力-全线接线图^3");
            FullLineDiagram4.Source = ImageCache.Get("电力-全线接线图^4");
        }
    }
}
