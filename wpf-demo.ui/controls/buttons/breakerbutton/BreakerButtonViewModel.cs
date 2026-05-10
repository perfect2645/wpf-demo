using wpf.ui.constants;
using wpf.ui.viewmodels.devices;
using wpf.ui.viewmodels.popup;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ioc;
using Logging;
using Messaging.LocalMessages;
using Messaging.LocalMessages.channel;
using Messaging.LocalMessages.CircuitItem;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;
using UserComponent.Core.devices;
using UserComponent.Core.route;
using WpfUtils.Consts;
using WpfUtils.Popup;

namespace wpf.ui.controls.buttons
{
    public partial class BreakerButtonViewModel : DeviceBaseVm
    {
        #region Properties

        private CancellationTokenSource _cts;
        private IDataChannelService _channelService;
        private IPopupService _popupService;

        [ObservableProperty]
        private BreakerState _breakerState;

        [ObservableProperty]
        private BreakerType _breakerType;

        [ObservableProperty]
        private string? _content;

        [ObservableProperty]
        private object? _actionParameter;

        [ObservableProperty]
        private bool _isEnabled = true;

        #region ICircuitItem

        private bool _isClose;
        public new bool IsClose
        {
            get { return _isClose; }
            set 
            { 
                _isClose = value;
                if (_isClose)
                {
                    BreakerState = BreakerState.Close;
                }
                else 
                {
                    BreakerState = BreakerState.Open;
                }
            }
        }

        #endregion ICircuitItem

        #endregion Properties

        #region Init

        [SetsRequiredMembers]
        public BreakerButtonViewModel(string stationId, string id)
        {
            Id = id;
            StationId = stationId;
            _popupService = AppContainer.ServiceProvider.GetRequiredKeyedService<IPopupService>(Consts.Ioc_Device);
            _cts = new CancellationTokenSource();
            _channelService = AppContainer.GetKeyedService<IDataChannelService>(CommonConsts.Ioc_CircuitChannel)!;
        }

        public void OnLoaded()
        {
            OnDataReceivedAsync();

            var routeService = AppContainer.GetService<IRouteService>();
        }


        public void SetupCircuitItem()
        {

        }

        #endregion Init

        #region Popup
        private bool CanButtonClick()
        {
            return true;
        }

        [RelayCommand]
        private void OnClick(string breakerId)
        {
            var popupSettings = new PopupSettings()
            {
                IsModal = true,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };

            var deviceVm = AppContainer.ServiceProvider.GetService<BreakerExecuteVm>();
            if (deviceVm == null)
            {
                Log4Logger.Logger.Error("Get BreakerExecuteVm failed.");
                return;
            }
            deviceVm.DeviceId = Id;
            deviceVm.DisplayName = DisplayName;
            deviceVm.DeviceName = DeviceName;
            deviceVm.StationId = StationId;
            deviceVm.StationName = StationName;
            deviceVm.Status = IsClose ? Consts.DeviceState_CS : Consts.DeviceState_OP;
            deviceVm.TargetStatus = IsClose ? Consts.DeviceState_OP : Consts.DeviceState_CS;
            deviceVm.ActionName = "执行";
            var popupVm = new DevicePopupViewModel(popupSettings, deviceVm);
            _popupService.ShowDialog(popupVm);
        }

        #endregion Popup

        #region Messaging

        private void UpdateBreakerState(ICircuitItem target)
        {
            if (target.StationId != StationId)
            {
                return;
            }

            if (target.Id != Id)
            {
                return;
            }

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                IsClose = target.IsClose;
            });
        }

        private async void OnDataReceivedAsync()
        {
            if (_channelService == null)
            {
                return;
            }
            var dataStream = _channelService.SubscribeAsync($"{StationId}{Id}", _cts.Token);

            try
            {
                await foreach (var data in dataStream!)
                {
                    //Log4Logger.Logger.Debug($"Update device message handling:[{data.StationId},{data.Id},{data.PowerOn}].");
                    DisplayName = data.DisplayName ?? DisplayName;
                    DeviceName = data.DeviceName ?? DeviceName;
                    StationName = data.StationName ?? StationName;
                    UpdateBreakerState(data);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex);
            }
        }

        #endregion Messaging

        #region Disposal

        public void Dispose()
        {
            //_cts?.Cancel();
            //_cts?.Dispose();
            _channelService?.Remove($"{StationId}{Id}");
        }

        #endregion Disposal
    }
}
