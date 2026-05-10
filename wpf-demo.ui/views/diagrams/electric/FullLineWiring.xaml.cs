using wpf.ui.viewmodels.diagrams.electric;
using Logging;
using System.Windows;
using System.Windows.Controls;

namespace wpf.ui.views.diagrams.electric
{
    /// <summary>
    /// FullLineWiring.xaml 的交互逻辑
    /// </summary>
    public partial class FullLineWiring : UserControl
    {
        public FullLineWiring()
        {
            InitializeComponent();
            Loaded += FullLineWiring_Loaded;

        }

        private void FullLineWiring_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as FullLineWiringVm;
            if (dataContext == null)
            {
                Log4Logger.Logger.Warn($"FullLineWiring Loaded failed.DataContext is not FullLineWiringVm.");
                return;
            }

            dataContext.OnLoaded();
        }
    }
}
