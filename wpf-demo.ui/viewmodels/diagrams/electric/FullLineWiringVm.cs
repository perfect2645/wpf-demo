using wpf.ui.constants;
using wpf.ui.model.payloads;
using wpf.ui.service.http;
using wpf.ui.service.mq;
using wpf.ui.viewmodels.diagrams.abstractions;
using wpf.ui.viewmodels.popup;
using wpf.ui.views.dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ioc;
using Logging;
using MaterialDesignThemes.Wpf;
using Messaging.Http.Exceptions;
using Messaging.LocalMessages.CircuitItem;
using Messaging.LocalMessages.events;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;
using PowerComponent.Core.Popup.ViewModel;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Threading;
using Utils;
using Utils.Enumerable;
using Utils.Object;
using WpfUtils.Popup;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class FullLineWiringVm : DiagramBase, ITrackNotification
    {

        #region Properties

        private IHttpCommandDataService _httpDataService;

        [ObservableProperty]
        private ISnackbarMessageQueue _trackMessageQueue;
        private MqService _mqService;
        private IPopupService _popupService;

        [ObservableProperty]
        private bool _isTrackNotificationEnabled;
        [ObservableProperty]
        private bool _isTrackNotificationVisible;
        [ObservableProperty]
        private Brush? _trackNotificationBackground;

        #endregion Properties

        public FullLineWiringVm(IHttpCommandDataService httpDataService,
            MqService mqService,
            [FromKeyedServices(Consts.Ioc_Diagram)]IPopupService popupService)
        {
            ViewId = Consts.Electric_FullWiring;
            _httpDataService = httpDataService;
            TrackMessageQueue = new SnackbarMessageQueue();
            _mqService = mqService;
            _mqService.UnRegisterMq(OnMqMessageReceived);
            _mqService.RegisterMq(OnMqMessageReceived);
            _popupService = popupService;

            IsTrackNotificationEnabled = true;
            IsTrackNotificationVisible = true;
        }

        #region Init

        public void OnLoaded()
        {
            Task.Run(() => LoadDataAsync(ViewId!));
        }

        #endregion Init

        #region LoadData

        private async Task LoadDataAsync(string stationId)
        {
            var data = await RunSearchAsync(stationId);

            var isValid = ValidateData(data);
            if (!isValid)
            {
                return;
            }
            await SetDataAsync(data!);
        }

        private async Task<TrackNetAsset?> RunSearchAsync(string stationId)
        {
            try
            {
                var url = "psc/run/query/track-net-asset";
                var httpContent = new HttpDataContent(url);
                return await _httpDataService.GetDataAsync<TrackNetAsset>(httpContent);
            }
            catch (HttpException ex)
            {
                Log4Logger.Logger.Error($"LoadData failed.HttpException:{ex.ErrCode}", ex);
                return null;
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"LoadData failed.", ex);
                return null;
            }
        }

        private bool ValidateData(TrackNetAsset? data)
        {
            if (data == null)
            {
                Log4Logger.Logger.Warn($"SetData failed. data is null.");
                return false;
            }

            return true;
        }

        private async Task SetDataAsync(TrackNetAsset data)
        {
            await UpdateTrackAsync(data.TrackList);
            await UpdateBusesAsync(data.BusList);
            await UpdateDevicesAsync(data.DeviceList);
        }

        private async Task UpdateDevicesAsync(List<Device> deviceList)
        {
            if (!deviceList.HasItem())
            {
                return;
            }

            Parallel.ForEach(deviceList, async device =>
            {
                var args = new CircuitEventArgs
                {
                    CircuitItem = new CircuitItem(device.StationCode.NotNullString(), device.DeviceCode)
                    {
                        PowerOn = device.PowerOn,
                        IsClose = device.PowerOn,
                        DisplayName = device.DisplayName,
                        DeviceName = device.DeviceName,
                        StationName = device.StationName
                    }
                };

                await CircuitEventManager.PublishAsync(ViewId, args);
            });

            await ValueTask.CompletedTask;
        }

        private async Task UpdateBusesAsync(List<Bus> busList)
        {
            if (!busList.HasItem())
            {
                return;
            }

            Parallel.ForEach(busList, async bus =>
            {
                var args = new CircuitEventArgs
                {
                    CircuitItem = new CircuitItem(bus.StationCode, bus.BusCode)
                    {
                        PowerOn = bus.PowerOn,
                        DeviceName = bus.BusName,
                        StationName = bus.StationName,
                    }
                };

                await CircuitEventManager.PublishAsync(ViewId, args);
            });

            await ValueTask.CompletedTask;
        }

        private async ValueTask UpdateTrackAsync(List<Track> trackList)
        {
            if (!trackList.HasItem())
            {
                return;
            }

            Parallel.ForEach(trackList, async track =>
            {
                var args = new CircuitEventArgs
                {
                    CircuitItem = new CircuitItem(string.Empty, track.TrackCode)
                    {
                        PowerOn = track.PowerOn,
                        DisplayName = track.DisplayName,
                    }
                };

                await CircuitEventManager.PublishAsync(ViewId, args);
            });

            await ValueTask.CompletedTask;
        }

        #endregion LoadData

        #region Mq

        private async Task OnMqMessageReceived(string routingKey, string message)
        {
            Log4Logger.Logger.Debug($"Mq message received:[{message}].");
            if (string.IsNullOrEmpty(message))
            {
                Log4Logger.Logger.Warn($"Mq message is empty.routingKey:{routingKey}.");
                return;
            }

            try
            {
                var device = JsonSerializer.Deserialize<DeviceChangeMessage>(message, JsonEncoder.JsonOption);
                if (device == null)
                {
                    Log4Logger.Logger.Warn($"Mq message deserialization failed.message:{message}.");
                    return;
                }
                if (device.DeviceCategory != nameof(DeviceCategory.TR))
                {
                    return;
                }

                await SendTrackNotification(device);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"MQ - Deserialize<DeviceChangeMessage> failed.[{message}]", ex);
            }
        }

        public async ValueTask SendTrackNotification(DeviceChangeMessage device)
        {
            if (!IsActive)
            {
                return;
            }
            if (device.TrackPowerOn.NotNullBool())
            {
                TrackNotificationBackground = Brushes.Green;
            }
            else
            {
                TrackNotificationBackground = Brushes.Red;
            }
            //TrackMessageQueue.Enqueue($"{device.SuccessMessage}-{device.TrackName}！");
            await ValueTask.CompletedTask;
        }

        private async Task TestMessage()
        {
            var isPowered = false;
            while (true)
            {
                await Task.Delay(5000);
                if (isPowered)
                {
                    TrackNotificationBackground = Brushes.Green;
                }
                else
                {
                    TrackNotificationBackground = Brushes.Red;
                }

                isPowered = !isPowered;
                TrackMessageQueue.Enqueue($"接触网断电-宋家庄上行！");
            }
        }

        protected void UnsubscribeMq()
        {
            _mqService.UnRegisterMq(OnMqMessageReceived);
        }

        #endregion Mq

        #region Popup

        [RelayCommand]
        private void OnPopup()
        {
            var popupContent = AppContainer.GetKeyedService<DiagramBase>(Consts.Electric_FullWiringPopup)!;

            var diagramPopupViewModel = new DiagramPopupViewModel(popupContent);
            diagramPopupViewModel.WindowStyle = System.Windows.WindowStyle.ToolWindow;
            _popupService.OnPopupupClosed += OnPopupClosed;
            _popupService.Show(diagramPopupViewModel);

            IsTrackNotificationEnabled = false;
        }

        private void OnPopupClosed(object? sender, EventArgs e)
        {
            IsTrackNotificationEnabled = true;
        }

        #endregion Popup
    }
}
