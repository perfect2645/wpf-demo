using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WpfUtils;

namespace wpf.ui.viewmodels.devices.abstractions
{
    public abstract partial class DeviceBase : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string? _deviceId;
        [ObservableProperty]
        private string? _displayName;
        [ObservableProperty]
        private string? _deviceName;
        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private string? _status;

        public Action? ClosePopupAction { get; set; }

        #endregion Properties
    }
}
