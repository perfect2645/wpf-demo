using wpf.ui.model.diagrams.electric;
using wpf.ui.model.payloads;
using wpf.ui.service.http;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logging;
using Messaging.Http.Exceptions;
using System.Collections.Generic;
using System.Windows.Media;
using Utils.Tasking;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class Power750PopupVm : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string? _title;
        [ObservableProperty]
        private Brush? _titleForeGround;

        private bool _isPowerOnView;
        [ObservableProperty]
        private List<Power750StationSelect>? _stations;
        private IHttpCommandDataService _httpDataService;

        #endregion Properties

        #region Init

        public Power750PopupVm(IHttpCommandDataService httpDataService, bool isPowerOnView)
        {
            _httpDataService = httpDataService;
            _isPowerOnView = isPowerOnView;
            InitView();
            LoadStations().SafeFireAndForget(OnSuccess, OnError);
        }

        private void InitView()
        {
            if (_isPowerOnView)
            {
                Title = "全线750V送电顺控";
                TitleForeGround = Brushes.Red;
            }
            else
            {
                Title = "全线750V停电顺控";
                TitleForeGround = Brushes.Blue;
            }
        }

        private async Task<List<Power750StationSelect>> LoadStations()
        {
            //http://localhost:38080/admin-api/psc/run/query/power-station-list
            var url = "psc/run/query/power-station-list";
            var httpContent = new HttpDataContent(url);
            var stationList = await _httpDataService.GetDataAsync<PowerStationList>(httpContent);
            if (stationList?.StationList != null)
            {
                Stations = stationList.StationList.Select(s => new Power750StationSelect
                {
                    StationCode = s.StationCode,
                    StationName = s.Name,
                    IsSelected = false
                }).ToList();
                return Stations;
            }
            else
            {
                Stations = new List<Power750StationSelect>();
                return Stations;
            }
        }
        private void OnSuccess(List<Power750StationSelect> stationList)
        {
        }

        private void OnError(Exception exception)
        {
            if (exception is HttpException httpException)
            {
                Log4Logger.Logger.Error($"LoadData failed.HttpException:{httpException.ErrCode}", httpException);
            }
            else
            {
                Log4Logger.Logger.Error($"LoadData failed.", exception);
            }
        }

        #endregion Init

        #region Commands

        [RelayCommand]
        private void OnStart()
        {

        }

        #endregion Commands
    }
}
