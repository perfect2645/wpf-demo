using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using UserComponent.Core.menu;

namespace wpf.ui.viewmodels.diagrams.electromechanical
{
    public partial class LightingVm : DiagramBase
    {
        public ICommand? ClickCommand { get; set; }

        public LightingVm()
        {
            ClickCommand = new RelayCommand<string>(OnPageChange);
        }

        private void OnPageChange(string parameter)
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
