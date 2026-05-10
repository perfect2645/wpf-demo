using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using PowerComponent.Core.Popup.ViewModel;

namespace wpf.ui.viewmodels.popup
{
    public partial class DiagramPopupViewModel : PopupViewModel
    {
        [ObservableProperty]
        private DiagramBase? _diagramViewModel;

        public DiagramPopupViewModel(DiagramBase diagramBase) :base(diagramBase.PopupSettings)
        {
            DiagramViewModel = diagramBase;
        }
    }
}
