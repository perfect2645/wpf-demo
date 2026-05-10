using wpf.ui.viewmodels.devices.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using PowerComponent.Core.Popup.ViewModel;
using WpfUtils.Popup;

namespace wpf.ui.viewmodels.popup
{
    public partial class DevicePopupViewModel : PopupViewModel
    {
        [ObservableProperty]
        private DeviceBase? _deviceViewModel;

        public DevicePopupViewModel(PopupSettings? popupSettings, DeviceBase? deviceViewModel) : base(popupSettings)
        {
            _deviceViewModel = deviceViewModel;
            InitDeviceViewModel();
        }

        private void InitDeviceViewModel()
        {
            if (DeviceViewModel == null)
            {
                return;
            }

            if (DeviceViewModel.ClosePopupAction == null)
            {
                DeviceViewModel.ClosePopupAction = new Action(OnClose);
            }
        }
    }
}
