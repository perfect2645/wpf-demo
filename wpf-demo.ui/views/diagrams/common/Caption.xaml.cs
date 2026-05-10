using System.Windows;
using System.Windows.Controls;

namespace wpf.ui.views.diagrams.common
{
    /// <summary>
    /// Caption.xaml 的交互逻辑
    /// </summary>
    public partial class Caption : UserControl
    {
        private static readonly Type CtrlType = typeof(Caption);

        public string StationName
        {
            get { return (string)GetValue(StationNameProperty); }
            set { SetValue(StationNameProperty, value); }
        }

        public static readonly DependencyProperty StationNameProperty =
            DependencyProperty.Register("StationName", typeof(string), CtrlType);

        public string CaptionText
        {
            get { return (string)GetValue(CaptionTextProperty); }
            set { SetValue(CaptionTextProperty, value); }
        }

        public static readonly DependencyProperty CaptionTextProperty =
            DependencyProperty.Register("CaptionText", typeof(string), CtrlType);

        public Caption()
        {
            InitializeComponent();
        }
    }
}
