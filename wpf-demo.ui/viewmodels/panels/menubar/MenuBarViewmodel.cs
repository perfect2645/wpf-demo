using wpf.ui.constants;
using wpf.ui.route;
using wpf.ui.viewmodels.panels.menubar;
using System.Collections.ObjectModel;
using UserComponent.Core.menu;
using UserComponent.Core.route;
using Utils;
using Utils.Enumerable;
using WpfUtils.Consts;

namespace wpf.ui.viewmodels.panels
{
    public class MenuBarViewmodel : MenuViewmodel
    {
        public MenuBarViewmodel() : base()
        {
        }

        #region MenuItems
        protected override void InitMenu()
        {
            PrimaryMenus = new ObservableCollection<MenuButtonBaseVm>(RouteCache.MenuList!);
        }

        #endregion MenuItems

        #region Actions

        protected override void UpdateViewContent()
        {
            if (SelectedSubMenu == null)
            {
                _ = ViewUpdateEventManager.PublishAsync(this, new ViewUpdateEventArgs(string.Empty, null));
                return;
            }

            var args = new Dictionary<string, object>();
            args.Add(CommonConsts.ViewUpdateType, ViewUpdateType.MenuChange);
            args.Add(CommonConsts.MenuId, SelectedSubMenu.Id.NotNullString());
            _ = ViewUpdateEventManager.PublishAsync(this, new ViewUpdateEventArgs(args!));
        }

        protected override void OnMenuSelected(object? sender, MenuEventArgs args)
        {
            base.OnMenuSelected(sender, args);

            var target = args?.SelectedMenu;
            if (target == null || target.IsFirstLevel)
            {
                return;
            }

            if (Consts.Electric_BtnId.Equals(target.Id)
                || Consts.EnvControl_Schedule.Equals(target.Id))
            {
                SubMenuPageChange(2);
                return;
            }

            if (Consts.BackToFullLineBtnId.Equals(target.Id))
            {
                SubMenuPageChange(1);
                return;
            }

            if (target.Content!.Contains("返回环控"))
            {
                SelectedPrimaryMenu = PrimaryMenus!.FirstOrDefault(menu => "环控".Equals(menu.Content));
                foreach (var menu in SelectedPrimaryMenu!.SubItems!.FirstOrDefault(subItem => "时间表".Equals(subItem.Content))!.SubItems!)
                {
                    var subMenu = (menu as MenuButtonViewMode)!;
                    subMenu.IsVisible = false;
                }
            }
        }

        private void SubMenuPageChange(int targetPage)
        {
            if (!SubMenus.HasItem())
            {
                return;
            }

            if (!SubMenus!.OfType<MenuButtonViewMode>().Any(menu => menu.Page > 1))
            {
                return;
            }

            foreach (var menu in SubMenus!)
            {
                var subMenu = (menu as MenuButtonViewMode)!;
                if (subMenu.Page == targetPage)
                {
                    subMenu.IsVisible = true;
                }
                else
                {
                    subMenu.IsVisible = false;
                }
            }
        }

        #endregion Actions
    }
}
