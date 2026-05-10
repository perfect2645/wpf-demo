using wpf.ui.model.payloads;
using wpf.ui.service.http;
using Ioc;
using Logging;
using Messaging.Http.Exceptions;
using Messaging.LocalMessages.channel;
using Messaging.LocalMessages.CircuitItem;
using Microsoft.Extensions.DependencyInjection;
using Utils;
using Utils.Enumerable;
using WpfUtils.Consts;

namespace wpf.ui.viewmodels.diagrams.abstractions
{
    public class PrimaryDiagram : DiagramBase
    {

        protected IHttpCommandDataService HttpDataService { get; }
        protected IDataChannelService DataChannelService { get; }
        public PrimaryDiagram(IHttpCommandDataService httpDataService,
            [FromKeyedServices(CommonConsts.Ioc_CircuitChannel)]IDataChannelService dataChannelService)
        {
            HttpDataService = httpDataService;
            DataChannelService = dataChannelService;
        }


        #region LoadData

        public async Task LoadDataAsync(string stationId)
        {
            var stationTask = Task.Run(() => LoadStationDataAsync(stationId));
            var trackTask = GetTrackDataAsync();

            await Task.WhenAll(stationTask, trackTask);
        }

        private async Task LoadStationDataAsync(string stationId)
        {
            var data = await GetStationDataAsync(stationId);
            var isValid = ValidateData(data);
            if (!isValid)
            {
                return;
            }
            await SetDataAsync(data);
        }

        private async Task GetTrackDataAsync()
        {
            try
            {
                var url = "psc/run/query/track-list";
                var httpContent = new HttpDataContent(url);
                var trackData = await HttpDataService.GetDataAsync<TrackAll>(httpContent);
                await SetTrackDataAsync(trackData?.TrackList);
            }
            catch (HttpException ex)
            {
                Log4Logger.Logger.Error($"Get track data failed.HttpException:{ex.ErrCode}", ex);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"Get track data failed.", ex);
            }
        }

        private async Task SetTrackDataAsync(List<Track>? tracks)
        {
            if (!tracks.HasItem())
            {
                throw new HttpException("GetTrackData failed. tracks is null or empty.", HttpStatus.DataEmpty);
            }

            foreach (var track in tracks!)
            {
                var item = new CircuitItem(string.Empty, track.TrackCode)
                {
                    DisplayName = track.DisplayName,
                    DeviceName = track.TrackName,
                    PowerOn = track.PowerOn,
                };
                await DataChannelService.SendDataAsync(item);
            }
        }

        protected async Task<StationCircuitData?> GetStationDataAsync(string stationId)
        {
            try
            {
                var httpContent = new HttpStationContent(stationId);
                return await HttpDataService.GetDataAsync<StationCircuitData>(httpContent);
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

        protected virtual bool ValidateData(StationCircuitData? data)
        {
            if (data == null)
            {
                Log4Logger.Logger.Warn($"SetData failed. data is null.");
                return false;
            }

            return true;
        }

        protected virtual async Task SetDataAsync(StationCircuitData? data)
        {
            var devices = data?.DeviceList;
            if (!devices.HasItem())
            {
                return;
            }

            DataChannelService.Log();
            foreach (var device in devices!)
            {
                var item = new CircuitItem(ViewId!, device.DeviceCode)
                {
                    IsClose = device.PowerOn,
                    DisplayName = device.DisplayName,
                    DeviceName = device.DeviceName,
                    StationName = device.StationName
                };
                await DataChannelService.SendDataAsync(item);
            }

            var buses = data!.BusList;
            if (!buses.HasItem())
            {
                return;
            }

            foreach (var bus in buses!)
            {
                var item = new CircuitItem(bus.StationCode!, bus.BusCode) { PowerOn = bus.PowerOn };
                await DataChannelService.SendDataAsync(item);
            }
        }

        #endregion LoadData
    }
}
