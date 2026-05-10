using UserComponent.Core.station;

namespace wpf.ui.viewmodels.panels.station
{
    public class StationViewModel : StationBaseVm
    {
        public StationViewModel(string id, string title) : base(id)
        {
            Title = title;
        }

        public StationViewModel(string id, string title, string displayName) : base(id)
        {
            Title = title;
            DisplayName = displayName;
        }
    }
}
