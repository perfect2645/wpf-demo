using wpf.ui.constants;
using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using UserComponent.Core.menu;

namespace wpf.ui.viewmodels.diagrams.envControl.mode
{
    public partial class StationBlockVm : DiagramBase
    {
        public ICommand? ClickCommand { get; set; }
        public ICommand? NextPageClickCommand { get; set; }
        public ICommand? PreviousPageClickCommand { get; set; }
        [ObservableProperty]
        private Visibility _firstPageVisibility;
        [ObservableProperty]
        private Visibility _secondPageVisibility;

        public StationBlockVm()
        {
            ClickCommand = new RelayCommand<string>(OnModeChange);
            NextPageClickCommand = new RelayCommand(OnNextPageButtonClick);
            PreviousPageClickCommand = new RelayCommand(OnPreviousPageButtonClick);
            FirstPageVisibility = Visibility.Visible;
            SecondPageVisibility = Visibility.Collapsed;
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

        private void OnNextPageButtonClick()
        {
            FirstPageVisibility = Visibility.Collapsed;
            SecondPageVisibility = Visibility.Visible;
        }

        private void OnPreviousPageButtonClick()
        {
            FirstPageVisibility = Visibility.Visible;
            SecondPageVisibility = Visibility.Collapsed;
        }
    }
}
