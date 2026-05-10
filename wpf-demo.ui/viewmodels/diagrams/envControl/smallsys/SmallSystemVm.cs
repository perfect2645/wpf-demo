using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace wpf.ui.viewmodels.diagrams.envControl
{
    public partial class SmallSystemVm : DiagramBase
    {
        [ObservableProperty]
        private bool isShowSouthern;
    }
}
