using wpf.ui.model.payloads;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace wpf.ui.viewmodels.diagrams.abstractions
{
    public interface ITrackNotification
    {
        ISnackbarMessageQueue TrackMessageQueue { get; set; }
        ValueTask SendTrackNotification(DeviceChangeMessage device);
        Brush TrackNotificationBackground { get; set; }
    }
}
