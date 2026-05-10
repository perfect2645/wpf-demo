using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Messaging.LocalMessages;
using System.Windows;
using WpfUtils.Popup;

namespace PowerComponent.Core.Popup.ViewModel
{
    public abstract partial class PopupViewModel : ObservableObject, IPopup
    {
        #region Properties

        public PopupSettings? PopupSettings { get; set; }

        [ObservableProperty]
        private WindowStyle _windowStyle;
        [ObservableProperty]
        private WindowStartupLocation _windowStartupLocation;

        #endregion Properties

        public PopupViewModel(PopupSettings? popupSettings) 
        {
            PopupSettings = popupSettings ?? new PopupSettings()
            {
                IsModal = true,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            ;
            InitPopupSettings();
        }

        private void InitPopupSettings()
        {
            if (PopupSettings == null)
            {
                return;
            }

            WindowStyle = PopupSettings.WindowStyle ?? WindowStyle.SingleBorderWindow;
            WindowStartupLocation = PopupSettings.WindowStartupLocation ?? WindowStartupLocation.Manual;
        }

        [RelayCommand]
        public virtual void OnClose()
        {
            WeakReferenceMessenger.Default.Send(new ClosePopupMessage(this));
        }
    }
}
