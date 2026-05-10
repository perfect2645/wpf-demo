using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace wpf.ui.viewmodels.diagrams.envControl
{
    public partial class BigSystemVm : DiagramBase
    {
        [ObservableProperty]
        private bool isShowSouthern;
    }
}
