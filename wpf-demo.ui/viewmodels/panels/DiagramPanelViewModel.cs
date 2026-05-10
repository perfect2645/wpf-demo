using wpf.ui.constants;
using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using wpf.ui.viewmodels.popup;
using Ioc;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;
using System.Windows.Controls;
using System.Windows.Threading;
using UserComponent.Core.menu;
using UserComponent.Core.route;
using Utils;
using Utils.Enumerable;
using WpfUtils.Attributes;
using WpfUtils.Consts;
using WpfUtils.Popup;

namespace wpf.ui.viewmodels.panels
{
    public partial class DiagramPanelViewModel
    {
        #region Properties

        private DiagramBase? _currentViewModel;
        public DiagramBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.SetIsActive(false);

                SetProperty(ref _currentViewModel, value);

                _currentViewModel?.SetIsActive(true);
            }
        }

        private IPopupService _popupService;
        private IRouteService _routeService;

        #endregion Properties

        #region Diagram ViewModel

        #endregion Diagram ViewModel

        public DiagramPanelViewModel()
        {
            _routeService = AppContainer.ServiceProvider.GetRequiredService<IRouteService>();
            ViewUpdateEventManager.SubscribeAsync(OnViewContentUpdateAsync);
            _popupService = AppContainer.ServiceProvider.GetRequiredKeyedService<IPopupService>(Consts.Ioc_Diagram);

            CurrentViewModel = RouteCache.DiagramMapping!.GetValueOrDefault("电力-车站一次系统图");
        }

        #region OnViewUpdate

        [Order(1)]
        private async ValueTask OnViewContentUpdateAsync(object? sender, ViewUpdateEventArgs e)
        {
            var viewUpdateType = e.Parameters.GetEnumValue<ViewUpdateType>(CommonConsts.ViewUpdateType);
            switch (viewUpdateType)
            {
                case ViewUpdateType.MenuChange:
                    await MenuUpdateHandler(e);
                    break;
                case ViewUpdateType.StationChange:
                    await StationUpdateHandler(e);
                    break;
                default:
                    break;
            }
        }

        private async Task MenuUpdateHandler(ViewUpdateEventArgs e)
        {
            var menuId = e.Parameters.Get(CommonConsts.MenuId).NotNullString();
            if (string.IsNullOrEmpty(menuId))
            {
                ImageSource = null;
                return;
            }

            bool? imageLoaded = await UpdateViewByImageAsync(menuId);
            if (imageLoaded == false)
            {
                ImageSource = null;
                UpdateViewByViewModel(menuId);
            }
            else
            {
                CurrentViewModel = null;
            }
        }

        private void UpdateViewByViewModel(string menuId)
        {
            if (!RouteCache.DiagramMapping!.Exists(menuId))
            {
                CurrentViewModel = null;
                return;
            }

            var matchedVm = RouteCache.DiagramMapping!.GetValueOrDefault(menuId);
            if (matchedVm?.PopupSettings != null)
            {
                ShowAsPopupHandler(matchedVm);
                return;
            }
            CurrentViewModel = matchedVm;
        }

        private bool? ShowAsPopupHandler(DiagramBase? matchedVm)
        {
            if (matchedVm is not IPopup popupVm)
            {
                return null;
            }

            var diagramPopupViewModel = new DiagramPopupViewModel(matchedVm);
            if (matchedVm.PopupSettings?.IsModal == true)
            {
                return _popupService.ShowDialog(diagramPopupViewModel);
            }
            else
            {
                _popupService.Show(diagramPopupViewModel);
                return null;
            }
        }

        private async ValueTask StationUpdateHandler(ViewUpdateEventArgs e)
        {
            var menuId = _routeService.SelectedSubMenu?.Id;
            if (menuId == Consts.Electric_PowerOn 
                || menuId == Consts.Electric_PowerOff)
            {
                var matchedVm = RouteCache.DiagramMapping!.GetValueOrDefault(menuId);
                await Dispatcher.CurrentDispatcher.InvokeAsync(() =>
                {
                    ShowAsPopupHandler(matchedVm);
                });
            }
        }

        #endregion OnViewUpdate
    }
}
