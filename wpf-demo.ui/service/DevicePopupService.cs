using wpf.ui.views.dialogs;
using PowerComponent.Core.Popup;
using PowerComponent.Core.Popup.ViewModel;

namespace wpf.ui.service
{
    public class DevicePopupService : IPopupService
    {
        private DevicePopup? _devicePopup;
        public EventHandler? OnPopupupClosed { get; set; }

        public DevicePopupService()
        {
        }

        public void Show(PopupViewModel? popupViewModel, string? title = null)
        {
            _devicePopup = new DevicePopup(popupViewModel);
            _devicePopup.Show();
            _devicePopup.Closed += OnPopupupClosed;
        }

        public bool? ShowDialog(string? title)
        {
            return null;
        }

        public bool? ShowDialog(PopupViewModel? popupViewModel, string? title = null)
        {
            _devicePopup = new DevicePopup(popupViewModel);
            return _devicePopup.ShowDialog();
        }
    }
}
