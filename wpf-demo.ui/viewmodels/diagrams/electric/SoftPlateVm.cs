using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class SoftPlateVm : DiagramBase
    {
        #region Properties

        [ObservableProperty]
        private ObservableCollection<SwitchSoftPlateItem>? _dataSource;

        #endregion Properties

        public SoftPlateVm()
        {
        }
    }

    public class SwitchSoftPlateItem
    {
        public string? SwitchName { get; set; }
        public string? Overcurrent { get; set; }
        public string? ZeroSequenceOvercurrent { get; set; }
        public string? Differential { get; set; }
    }
}
