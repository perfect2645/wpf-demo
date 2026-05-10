using wpf.ui.viewmodels.devices.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WpfUtils;

namespace wpf.ui.viewmodels.diagrams.envControl
{
    public partial class ModePopupVm : DeviceBase
    {
        #region Properties

        [ObservableProperty]
        private bool _isOpen;

        public Action<string>? ModePopupAction { get; set; }

        #endregion Properties
        public ModePopupVm() 
        {
            DeviceName = "ttd_bas_kt_ll_rs";
            Description = "天坛东 - 北端环控电控室空调机KT-ll-1";
            Status = "运行";
        }

        [RelayCommand]
        private void OnDetail()
        {
            ModePopupAction?.Invoke("详情");
        }
    }
}
