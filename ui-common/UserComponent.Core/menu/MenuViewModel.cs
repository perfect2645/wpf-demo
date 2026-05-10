using Ioc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using UserComponent.Core.route;
using WpfUtils;

namespace UserComponent.Core.menu
{
    public abstract class MenuViewmodel : NotifyChanged
    {
        #region Properties

        private IRouteService _routeService;

        private ObservableCollection<MenuButtonBaseVm>? _primaryMenus;
        public ObservableCollection<MenuButtonBaseVm>? PrimaryMenus
        {
            get { return _primaryMenus; }
            set
            {
                _primaryMenus = value;
                NotifyUI(() => PrimaryMenus);
            }
        }

        private ObservableCollection<MenuButtonBaseVm>? _subMenus;
        public ObservableCollection<MenuButtonBaseVm>? SubMenus
        {
            get { return _subMenus; }
            set
            {
                _subMenus = value;
                NotifyUI(() => SubMenus);
            }
        }

        public MenuButtonBaseVm? SelectedPrimaryMenu
        {
            get => _routeService.SelectedPrimaryMenu;
            set
            {
                _routeService.SetPrimaryMenu(value);
                NotifyUI(() => SelectedPrimaryMenu);
                BuildSubMenu();
            }
        }

        public MenuButtonBaseVm? SelectedSubMenu
        {
            get => _routeService.SelectedSubMenu;
            set
            {
                _routeService.SetSubMenu(value);
                NotifyUI(() => SelectedPrimaryMenu);
                UpdateViewContent();
            }
        }

        #endregion Properties

        public MenuViewmodel()
        {
            _routeService = AppContainer.ServiceProvider.GetRequiredService<IRouteService>();
            InitMenu();
            MenuEventManager.Subscribe(OnMenuSelected);
        }

        protected abstract void InitMenu();

        protected virtual void OnMenuSelected(object? sender, MenuEventArgs args) 
        {
            var target = args?.SelectedMenu;
            if (target == null)
            {
                SelectedPrimaryMenu = null;
                SelectedSubMenu = null;
                return;
            }

            if (target.IsFirstLevel || "时间表".Equals(target.Content))
            {
                SelectedPrimaryMenu = args!.SelectedMenu;
            }
            else
            {
                SelectedSubMenu = args!.SelectedMenu;
            }
        }

        protected virtual void BuildSubMenu()
        {
            SelectedSubMenu = null;
            SubMenus = SelectedPrimaryMenu?.SubItems;
            UpdateViewContent();
        }

        protected abstract void UpdateViewContent();
    }
}
