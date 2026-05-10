using PowerComponent.Core.Popup.ViewModel;

namespace PowerComponent.Core.Popup
{
    public interface IPopupService
    {
        bool? ShowDialog(string? title = null);
        bool? ShowDialog(PopupViewModel? PopupViewModel, string? title = null);
        void Show(PopupViewModel? popupViewModel, string? title = null);
        EventHandler? OnPopupupClosed { get; set; }
    }
}
