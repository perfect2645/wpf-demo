using System.Windows;

namespace WpfUtils.Popup
{
    public class PopupSettings
    {
        public Window? Owner { get; set; }
        public WindowStyle? WindowStyle { get; set; }
        public WindowStartupLocation? WindowStartupLocation { get; set; }
        public bool IsModal { get; set; } = true;
        public double Width { get; set; }
        public double Height { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
    }

    public interface IPopup
    {
        PopupSettings? PopupSettings { get; set; }
    }
}
