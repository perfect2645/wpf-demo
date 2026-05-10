using CommunityToolkit.Mvvm.ComponentModel;

namespace PowerComponent.Core.Popup
{
    public interface ICustomPopupService
    {
        EventHandler? OnPopupupClosed { get; set; }
        void Show(ObservableObject contentViewModel);
        bool? ShowDialog(ObservableObject contentViewModel);
    }
}
