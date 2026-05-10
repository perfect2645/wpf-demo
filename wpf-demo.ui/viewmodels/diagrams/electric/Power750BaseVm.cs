using wpf.ui.constants;
using wpf.ui.model.payloads;
using wpf.ui.service.http;
using wpf.ui.service.mq;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logging;
using Messaging.Http.Exceptions;
using Messaging.LocalMessages.CircuitItem;
using Messaging.LocalMessages.events;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;
using Utils;
using Utils.Enumerable;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class Power750BaseVm : DiagramBase
    {
        private ICustomPopupService _popupService;
        private IHttpCommandDataService _httpDataService;
        private MqService _mqService;

        [ObservableProperty]
        private bool _isPowerOnView;
        public Power750BaseVm([FromKeyedServices(Consts.Ioc_CustomPopup)]ICustomPopupService popupService,
            IHttpCommandDataService httpDataService,
            MqService mqService)
        {
            _popupService = popupService;
            _httpDataService = httpDataService;
            _mqService = mqService;
        }

        #region Popup
        [RelayCommand]
        private void OnSelectLine()
        {
            var popupVm = new Power750PopupVm(_httpDataService, IsPowerOnView);
            _popupService.ShowDialog(popupVm);
        }

        [RelayCommand]
        private void OnStop()
        {

        }

        #endregion Popup

        #region Load data

        public void OnLoaded()
        {
            Task.Run(() => LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            var data = await RunSearchAsync();

            var isValid = ValidateData(data);
            if (!isValid)
            {
                return;
            }
            await SetDataAsync(data!);
        }

        private async Task<TrackNetAsset?> RunSearchAsync()
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

        #endregion Load data
    }
}
