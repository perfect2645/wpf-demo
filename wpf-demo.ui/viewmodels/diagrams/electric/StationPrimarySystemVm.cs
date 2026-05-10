using wpf.ui.constants;
using wpf.ui.model.payloads;
using wpf.ui.service.mq;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using Ioc;
using Logging;
using MaterialDesignThemes.Wpf;
using Messaging.LocalMessages.CircuitItem;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Threading;
using UserComponent.Core.menu;
using UserComponent.Core.route;
using Utils;
using Utils.Enumerable;
using Utils.Object;
using WpfUtils.Attributes;
using WpfUtils.Consts;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class StationPrimarySystemVm : DiagramBase, ITrackNotification
    {
        #region Properties

        public Dictionary<string, ICircuitItem> CircuitItems { get; } = new();

        private PrimaryDiagram? _currentViewModel;
        public PrimaryDiagram? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.SetIsActive(false);

                SetProperty(ref _currentViewModel, value);

                _currentViewModel?.SetIsActive(true);
            }
        }

        [ObservableProperty]
        private ISnackbarMessageQueue _trackMessageQueue;
        [ObservableProperty]
        private Brush? _trackNotificationBackground;

        private MqService _mqService;

        #endregion Properties

        public StationPrimarySystemVm(MqService mqService)
        {
            ViewUpdateEventManager.SubscribeAsync(OnViewContentUpdateAsync);
            TrackMessageQueue = new SnackbarMessageQueue();
            _mqService = mqService;
            _mqService.UnRegisterMq(OnMqMessageReceived);
            _mqService.RegisterMq(OnMqMessageReceived);
        }

        [Order(9)]
        private async ValueTask OnViewContentUpdateAsync(object? sender, ViewUpdateEventArgs args)
        {
            if (!IsActive)
            {
                return;
            }
            var viewUpdateType = args.Parameters.GetEnumValue<ViewUpdateType>(CommonConsts.ViewUpdateType);
            switch (viewUpdateType)
            {
                case ViewUpdateType.MenuChange:
                    await MenuUpdateHandler(args);
                    break;
                case ViewUpdateType.StationChange:
                    await StationUpdateHandler(args);
                    break;
                default:
                    break;
            }
        }

        private async Task MenuUpdateHandler(ViewUpdateEventArgs args)
        {
            var routeService = AppContainer.GetService<IRouteService>()!;
            var stationId = routeService.SelectedStation?.Id;
            if (stationId == null)
            {
                CurrentViewModel = null;
                Log4Logger.Logger.Error($"未找到车站一次系统图对应的StationId=[{stationId}]");
                return;
            }
            await UpdateCurrentView(stationId);
        }

        private async ValueTask StationUpdateHandler(ViewUpdateEventArgs args)
        {
            var stationId = args.Parameters.Get(CommonConsts.StationId).NotNullString();
            await UpdateCurrentView(stationId);
        }

        private async ValueTask UpdateCurrentView(string stationId)
        {
            try
            {
                var matchedVm = AppContainer.GetKeyedService<PrimaryDiagram>(stationId);
                if (matchedVm == null)
                {
                    CurrentViewModel = null;
                    Log4Logger.Logger.Error($"未找到车站一次系统图对应的ViewModel, StationId=[{stationId}]");
                    return;
                }

                await Dispatcher.CurrentDispatcher.InvokeAsync(() =>
                {
                    CurrentViewModel = matchedVm;
                });
                if (CurrentViewModel != null)
                {
                    await CurrentViewModel.LoadDataAsync(stationId);
                }
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex);
                CurrentViewModel = null;
            }
        }

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
            while(true)
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

        #endregion Mq
    }
}
