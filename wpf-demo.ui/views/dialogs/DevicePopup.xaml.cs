using CommunityToolkit.Mvvm.Messaging;
using Logging;
using Messaging.LocalMessages;
using PowerComponent.Core.Popup.ViewModel;
using System.Windows;

namespace wpf.ui.views.dialogs
{
    /// <summary>
    /// DevicePopup.xaml 的交互逻辑
    /// </summary>
    public partial class DevicePopup : Window
    {
        public DevicePopup(PopupViewModel? DevicePopupViewModel)
        {
            InitializeComponent();
            RenderTransform = null;
            Owner = Application.Current.MainWindow;
            DataContext = DevicePopupViewModel;

            WindowStartupLocation = DevicePopupViewModel?.WindowStartupLocation ?? WindowStartupLocation.Manual;

            WeakReferenceMessenger.Default.Register<ClosePopupMessage>(this, (_, message) =>
            {
                OnCloseMessage(message);
            });
        }

        private void OnCloseMessage(ClosePopupMessage message)
        {
            if (message.Sender?.Target == DataContext)
                Close();
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
                Top = Owner.Top + 242;
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex.Message);
            }
        }
    }
}
