using UserComponent.Core.menu;
using UserComponent.Core.station;

namespace UserComponent.Core.route
{
    public interface IRouteService
    {
        MenuButtonBaseVm? SelectedPrimaryMenu { get; }
        MenuButtonBaseVm? SelectedSubMenu { get; }
        void SetPrimaryMenu(MenuButtonBaseVm? menu);
        void SetSubMenu(MenuButtonBaseVm? menu);
        StationBaseVm? SelectedStation { get; }
        void SetStation(StationBaseVm? station);
    }
}
