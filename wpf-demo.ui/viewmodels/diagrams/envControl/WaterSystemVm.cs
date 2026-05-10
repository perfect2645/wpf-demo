using wpf.ui.constants;
using wpf.ui.route;
using wpf.ui.viewmodels.devices;
using wpf.ui.viewmodels.devices.abstractions;
using wpf.ui.viewmodels.diagrams.abstractions;
using wpf.ui.viewmodels.popup;
using CommunityToolkit.Mvvm.Input;
using Ioc;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;
using System.Windows.Input;
using UserComponent.Core.menu;
using WpfUtils.Popup;

namespace wpf.ui.viewmodels.diagrams.envControl
{
    public partial class WaterSystemVm : DiagramBase
    {
        #region Properties

        public ICommand ControlLeftButtonUpCommand { get; set; }

        public ModePopupVm ModePopupVm { get; set; }

        private IPopupService _popupService;

        private Action<string>? modePopupAction { get; set; }

        #endregion Properties

        public WaterSystemVm() : base()
        {
            ControlLeftButtonUpCommand = new RelayCommand<MouseButtonEventArgs>(OnControlClick);
            _popupService = AppContainer.ServiceProvider.GetRequiredKeyedService<IPopupService>(Consts.Ioc_Device);

            modePopupAction = new Action<string>(OnModeAction);
            ModePopupVm = new ModePopupVm() { ModePopupAction = modePopupAction };
        }

        #region Device Popup

        private void OnControlClick(MouseButtonEventArgs? e)
        {
            ModePopupVm.IsOpen = true;
        }

        private void OnModeAction(string actionType)
        {
            switch (actionType)
            {
                case "详情":
                    OpenDetail();
                    break;
                default:break;
            }
        }

        private void OpenDetail()
        {
            var deviceVm = new DeviceDetailVm()
            {
                DeviceName = "ttd_bas_kt_ll_rs",
                Description = "天坛东 - 北端环控电控室空调机KT-ll-1",
                Status = "运行"
            };
            ShowAsPopupHandler(deviceVm);
        }

        private bool? ShowAsPopupHandler(DeviceBase deviceVm)
        {
            var popupSettings = new PopupSettings()
            {
                Left = 200,
                Top = 200,
                IsModal = true,
            };
            var popupViewModel = new DevicePopupViewModel(popupSettings, deviceVm);
            return _popupService.ShowDialog(popupViewModel);
        }

        #endregion Device Popup

        #region Navigation

        [RelayCommand]
        private void OnWaterSystemMode()
        {
            var targetMenu = RouteCache.FindMenuById(RouteCache.MenuList, Consts.Environment_BigSys);
            if (targetMenu == null)
            {
                return;
            }
            var menuEventArgs = new MenuEventArgs(targetMenu);
            MenuEventManager.Publish(this, menuEventArgs);
        }

        #endregion Navigation
    }
}
