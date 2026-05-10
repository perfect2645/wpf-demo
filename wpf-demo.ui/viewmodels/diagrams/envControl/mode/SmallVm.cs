using wpf.ui.constants;
using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UserComponent.Core.menu;

namespace wpf.ui.viewmodels.diagrams.envControl.mode
{
    public partial class SmallVm : DiagramBase
    {
        public ICommand? ClickCommand { get; set; }

        public SmallVm()
        {
            ClickCommand = new RelayCommand<string>(OnModeChange);
        }

        private void OnModeChange(string parameter)
        {
            var targetMenu = RouteCache.FindMenuById(RouteCache.MenuList, parameter);
            if (targetMenu == null)
            {
                return;
            }
            var menuEventArgs = new MenuEventArgs(targetMenu);
            MenuEventManager.Publish(this, menuEventArgs);
        }
    }
}
