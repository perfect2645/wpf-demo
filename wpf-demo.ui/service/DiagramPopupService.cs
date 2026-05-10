using wpf.ui.views.dialogs;
using PowerComponent.Core.Popup;
using PowerComponent.Core.Popup.ViewModel;

namespace wpf.ui.service
{
    public class DiagramPopupService: IPopupService
    {
        public EventHandler? OnPopupupClosed { get; set; }
        public DiagramPopupService() 
        {
        }

        public void Show(PopupViewModel? popupViewModel, string? title = null)
        {
            var diagramWindow = new DiagramPopup(popupViewModel);
            diagramWindow.Show();
            diagramWindow.Closed += OnPopupupClosed;
        }

        public bool? ShowDialog(string? title)
        {
            return null;
        }

        public bool? ShowDialog(PopupViewModel? popupViewModel, string? title = null)
        {
            var diagramWindow = new DiagramPopup(popupViewModel);
            return diagramWindow.ShowDialog();
        }
    }
}
