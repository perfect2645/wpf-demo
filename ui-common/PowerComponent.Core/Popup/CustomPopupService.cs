using CommunityToolkit.Mvvm.ComponentModel;
using PowerComponent.Core.Popup.view;
using PowerComponent.Core.Popup.ViewModel;

namespace PowerComponent.Core.Popup
{
    public class CustomPopupService : ICustomPopupService
    {
        private CustomPopup? _customPopup;
        public EventHandler? OnPopupupClosed { get; set; }

        public CustomPopupService()
        {
        }

        public void Show(ObservableObject contentViewModel)
        {
            var popupViewModel = new CustomPopupViewModel(contentViewModel);
            _customPopup = new CustomPopup(popupViewModel);
            _customPopup.Show();
            _customPopup.Closed += OnPopupupClosed;
        }

        public bool? ShowDialog(ObservableObject contentViewModel)
        {
            var popupViewModel = new CustomPopupViewModel(contentViewModel);
            _customPopup = new CustomPopup(popupViewModel);
            return _customPopup.ShowDialog();
        }
    }
}
