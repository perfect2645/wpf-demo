using wpf.ui.viewmodels.popup;
using Logging;
using PowerComponent.Core.Popup.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace wpf.ui.views.dialogs
{
    /// <summary>
    /// DiagramPopup.xaml 的交互逻辑
    /// </summary>
    public partial class DiagramPopup : Window
    {
        public DiagramPopup(PopupViewModel? diagramPopupViewModel)
        {
            InitializeComponent();
            RenderTransform = null;
            Owner = Application.Current.MainWindow;
            DataContext = diagramPopupViewModel;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            try
            {
                var dataContext = DataContext as PopupViewModel;
                if (dataContext?.PopupSettings?.IsModal == false)
                {
                    Visibility = Visibility.Hidden;
                    Dispatcher.BeginInvoke(() =>
                    {
                        Close();
                    });

                    Owner.Activate();

                    //if (Owner != null)
                    //{
                    //    Owner.Dispatcher.BeginInvoke(new Action(() =>
                    //    {
                    //        Owner.Activate();

                    //    }), System.Windows.Threading.DispatcherPriority.Send);
                    //}
                }
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                Left = Owner.Left;
                Top = Owner.Top + 242;

                if (DataContext is DiagramPopupViewModel popupViewModel)
                {
                    popupViewModel.DiagramViewModel?.SetIsActive(true);
                }
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex.Message);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (DataContext is DiagramPopupViewModel popupViewModel)
            {
                popupViewModel.DiagramViewModel?.SetIsActive(false);
            }
            base.OnClosed(e);
        }
    }
}
