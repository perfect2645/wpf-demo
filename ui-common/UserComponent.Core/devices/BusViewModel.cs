using Ioc;
using Logging;
using Messaging.LocalMessages;
using Messaging.LocalMessages.channel;
using Messaging.LocalMessages.CircuitItem;
using Messaging.LocalMessages.events;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;
using WpfUtils.Consts;

namespace UserComponent.Core.devices
{
    public partial class BusViewModel : DeviceBaseVm
    {
        #region Properties

        private CancellationTokenSource? _cts;
        private IDataChannelService? _channelService;

        #endregion Properties

        #region Init

        [SetsRequiredMembers]
        public BusViewModel(string stationId, string id)
        {
            Id = id;
            StationId = stationId;

        }
        public void OnLoaded()
        {
            if (TransationMode == TransationMode.Channel)
            {
                _cts = new CancellationTokenSource();
                _channelService = AppContainer.GetKeyedService<IDataChannelService>(CommonConsts.Ioc_CircuitChannel);
                OnChannelDataReceivedAsync();
                return;
            }
            else if (TransationMode == TransationMode.Event)
            {
                CircuitEventManager.SubscribeAsync(OnEventDataReveivedAsync);
            }
        }

        #endregion Init

        #region Messaging
        private void UpdateDeviceState(ICircuitItem target)
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
                PowerOn = target.PowerOn;
            });
        }

        private async void OnChannelDataReceivedAsync()
        {
            if (_channelService == null)
            {
                return;
            }
            var dataStream = _channelService.SubscribeAsync($"{StationId}{Id}", _cts!.Token);

            try
            {
                await foreach (var data in dataStream!)
                {
                    Log4Logger.Logger.Debug($"Update bus message handling:[{data.StationId},{data.Id},{data.PowerOn}].");
                    UpdateDeviceState(data);
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

        private async ValueTask OnEventDataReveivedAsync(object? sender, CircuitEventArgs args)
        {
            var data = args.CircuitItem;
            if (data == null)
            {
                return;
            }

            if (Id != data.Id || StationId != data.StationId)
            {
                return;
            }

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                DisplayName = data.DisplayName ?? DisplayName;
                DeviceName = data.DeviceName ?? DeviceName;
                UpdateDeviceState(data);
            });

            await ValueTask.CompletedTask;
        }

        #endregion Messaging

        #region Disposal

        public void Dispose()
        {
            if (TransationMode == TransationMode.Channel)
            {
                //_cts?.Cancel();
                //_cts?.Dispose();
                _channelService?.Remove($"{StationId}{Id}");
            }
            else if (TransationMode == TransationMode.Event)
            {
                CircuitEventManager.UnSubscribeAsync(OnEventDataReveivedAsync);
            }
        }

        #endregion Disposal
    }
}
