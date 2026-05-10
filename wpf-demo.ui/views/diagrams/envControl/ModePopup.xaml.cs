using Logging;
using System.Windows.Controls.Primitives;

namespace wpf.ui.views.diagrams.envControl
{
    /// <summary>
    /// ModePopup.xaml 的交互逻辑
    /// </summary>
    public partial class ModePopup : Popup
    {
        public ModePopup()
        {
            InitializeComponent();
        }

        private void Popup_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                VerticalOffset = 260;
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex.Message);
            }
        }
    }
}
