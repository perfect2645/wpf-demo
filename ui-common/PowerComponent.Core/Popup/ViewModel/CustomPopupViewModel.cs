using CommunityToolkit.Mvvm.ComponentModel;

namespace PowerComponent.Core.Popup.ViewModel
{
    public partial class CustomPopupViewModel : PopupViewModel
    {
        [ObservableProperty]
        private ObservableObject? _contentViewModel;
        public CustomPopupViewModel(ObservableObject contentViewModel) : base(null)
        {
            _contentViewModel = contentViewModel;
        }
    }
}
