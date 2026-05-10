using Ioc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using UserComponent.Core.route;
using WpfUtils;

namespace UserComponent.Core.station
{
    public abstract class StationPanelBaseVm : NotifyChanged
    {
        #region Properties

        private IRouteService _routeService;

        private ObservableCollection<StationBaseVm> _stations = new();
        public ObservableCollection<StationBaseVm> Stations
        {
            get => _stations;
            set
            {
                _stations = value;
                NotifyUI(() => Stations);
            }
        }

        public StationBaseVm? SelectedStation
        {
            get => _routeService.SelectedStation;
            set
            {
                _routeService.SetStation(value);
                NotifyUI(() => SelectedStation);
                UpdateViewContent();
    }
        }

        #endregion Properties

        #region Init

        public StationPanelBaseVm()
        {
            _routeService = AppContainer.ServiceProvider.GetRequiredService<IRouteService>();
            StationEventManager.Subscribe(OnStationSelected);
            InitStations();
        }

        protected abstract void InitStations();

        #endregion Init

        #region Station Selected

        protected virtual void OnStationSelected(object? sender, StationEventArgs args)
        {
            var target = args?.SelectedStation;
            if (target == null)
            {
                return;
            }

            if (target == SelectedStation)
            {
                return;
            }

            Stations.Where(s => s != target).ToList().ForEach(s => s.IsSelected = false);
            SelectedStation = target;
        }

        #endregion Station Selected

        protected abstract void UpdateViewContent();

    }
}
