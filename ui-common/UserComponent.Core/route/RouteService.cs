using Logging;
using UserComponent.Core.menu;
using UserComponent.Core.station;
using Utils;
using WpfUtils;

namespace UserComponent.Core.route
{
    public class RouteService : NotifyChanged, IRouteService
    {
        #region Menu

        private MenuButtonBaseVm? _selectedPrimaryMenu;
        private MenuButtonBaseVm? _selectedSubMenu;
        public MenuButtonBaseVm? SelectedPrimaryMenu => _selectedPrimaryMenu;
        public MenuButtonBaseVm? SelectedSubMenu => _selectedSubMenu;
        public void SetPrimaryMenu(MenuButtonBaseVm? menu)
        {
            if (_selectedPrimaryMenu == menu)
            {
                return;
            }
            _selectedPrimaryMenu = menu;
            if (_selectedPrimaryMenu != null)
            {
                _selectedPrimaryMenu.IsSelected = true;
            }
        }
        public void SetSubMenu(MenuButtonBaseVm? menu)
        {
            if (_selectedSubMenu == menu)
            {
                return;
            }
            _selectedSubMenu = menu;
            if (_selectedSubMenu != null)
            {
                _selectedSubMenu.IsSelected = true;
            }
        }

        #endregion Menu

        #region Station

        public StationBaseVm? SelectedStation => _selectedStation;
        private StationBaseVm? _selectedStation;
        public void SetStation(StationBaseVm? station)
        {
            if (_selectedStation == station)
            {
                return;
            }
            _selectedStation = station;
            if (_selectedStation != null)
            {
                _selectedStation.IsSelected = true;
            }
        }

        private string? _stationName;
        public string? StationName
        {
            get => _stationName;
            set
            {
                if (_stationName != value)
                {
                    _stationName = value;
                }
                NotifyUI(() => StationName);
            }
        }


        private void OnStationChanged(object? sender, StationEventArgs e)
        {
            var station = e.SelectedStation;
            if (station == null)
            {
                Log4Logger.Logger.Warn($"Station is null");
                return;
            }

            StationName = station.DisplayName ?? station.Title.NotNullString();
        }

        #endregion Station

        public RouteService()
        {
            StationEventManager.Subscribe(OnStationChanged);
        }
    }
}
