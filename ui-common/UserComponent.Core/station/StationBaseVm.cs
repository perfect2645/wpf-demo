using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WpfUtils;

namespace UserComponent.Core.station
{
    public abstract class StationBaseVm : NotifyChanged
    {
        #region Properties

        public string Id { get; init; }
        public string? Linkage { get; init; }
        public ICommand? ClickCommand { get; }
        public object? ClickCommandParameter { get; set; }

        private string? _title;
        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyUI(() => Title);
            }
        }

        public string? DisplayName { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                {
                    return;
                }
                _isSelected = value;
                NotifyUI(() => IsSelected);
            }
        }

        private bool _showLine = true;
        public bool ShowLine
        {
            get => _showLine;
            set
            {
                if (_showLine == value)
                {
                    return;
                }
                _showLine = value;
                NotifyUI(() => ShowLine);
            }
        }

        #endregion Properties

        #region Init

        public StationBaseVm(string id)
        {
            Id = id;
            ClickCommand = new RelayCommand<object>(OnStationClick, CanStationClick);
        }

        #endregion Init

        #region Station Selected

        private bool CanStationClick(object? parameter)
        {
            return !IsSelected;
        }

        private void OnStationClick(object? parameter)
        {
            var stationEventArgs = new StationEventArgs(this);
            StationEventManager.Publish(this, stationEventArgs);
        }

        #endregion Station Selected
    }
}
