using CommunityToolkit.Mvvm.ComponentModel;

namespace wpf.ui.model.diagrams.electric
{
    public partial class Power750StationSelect : ObservableObject
    {
        public required string StationCode { get; set; }

        [ObservableProperty]
        private string? _stationName;

        [ObservableProperty]
        private bool _isSelected;
    }
}
