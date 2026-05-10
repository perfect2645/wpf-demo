using wpf.ui.model.payloads;
using wpf.ui.viewmodels.diagrams.abstractions;
using wpf.ui.views.diagrams.envControl.bigsys;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace wpf.ui.viewmodels.diagrams.envControl
{
    public class BigSystemNorthern : DiagramBase
    {
        public ICommand? ZSFDoubleClickCommand { get; set; }

        public BigSystemNorthern() : base()
        {
            ZSFDoubleClickCommand = new RelayCommand(OnZSFButtonClick, CanButtonClick);
        }

        private void OnZSFButtonClick()
        {
            var window = new ZSFDeviceControlWindow();
            window.ShowDialog();

        }

        private bool CanButtonClick()
        {
            return true;
        }
    }
}
