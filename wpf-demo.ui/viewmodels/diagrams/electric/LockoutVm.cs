using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using Logging;
using UserComponent.Core.station;
using Utils;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public class LockoutVm : DiagramBase
    {
        public LockoutVm()
        {
            StationEventManager.Subscribe(OnStationSelected);
        }

        private void OnStationSelected(object? sender, StationEventArgs e)
        {
            var station = e.SelectedStation;
            if (station == null)
            {
                Log4Logger.Logger.Warn($"Station is null");
                return;
            }
        }
    }
}
