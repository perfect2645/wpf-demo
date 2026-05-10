using wpf.ui.viewmodels.diagrams.electric;
using Logging;
using System.Windows;
using System.Windows.Controls;

namespace wpf.ui.views.diagrams.electric
{
    /// <summary>
    /// Power750On.xaml 的交互逻辑
    /// </summary>
    public partial class Power750On : UserControl
    {
        public Power750On()
        {
            InitializeComponent();

            Loaded += Power750On_Loaded;
        }

        private void Power750On_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as Power750BaseVm;
            if (dataContext == null)
            {
                Log4Logger.Logger.Warn($"Power750On Loaded failed.DataContext is not Power750BaseVm.");
                return;
            }

            dataContext.OnLoaded();
        }
    }
}
