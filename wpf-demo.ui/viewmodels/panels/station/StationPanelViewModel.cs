using wpf.ui.constants;
using wpf.ui.route;
using System.Collections.ObjectModel;
using UserComponent.Core.menu;
using UserComponent.Core.station;
using Utils;
using WpfUtils.Consts;

namespace wpf.ui.viewmodels.panels.station
{
    public class StationPanelViewModel : StationPanelBaseVm
    {
        protected override void InitStations()
        {
            Stations = new ObservableCollection<StationBaseVm>(RouteCache.StationList!);

            var defaultStation = Stations.FirstOrDefault(s => s.Id.Equals(Consts.ParkingLot));
            var stationEventArgs = new StationEventArgs(defaultStation);
            StationEventManager.Publish(this, stationEventArgs);
        }

        protected override void UpdateViewContent()
        {
            if (SelectedStation == null)
            {
                _ = ViewUpdateEventManager.PublishAsync(this, new ViewUpdateEventArgs(string.Empty, null));
                return;
            }

            var args = new Dictionary<string, object>();
            args.Add(CommonConsts.ViewUpdateType, ViewUpdateType.StationChange);
            args.Add(CommonConsts.StationId, SelectedStation.Id.NotNullString());
            _ = ViewUpdateEventManager.PublishAsync(this, new ViewUpdateEventArgs(args!));
        }
    }
}
