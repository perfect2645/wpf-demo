using WpfUtils;

namespace wpf.ui.model.diagrams.electric
{
    public class RailPotentialItem : NotifyChanged
    {
        public string StationName { get; set; }

        private RailPotentialState _railPotentialState;
        public RailPotentialState RailPotentialState
        {
            get => _railPotentialState;
            set
            {
                if (_railPotentialState != value)
                {
                    _railPotentialState = value;
                    NotifyUI(() => RailPotentialState);
                }
            }
        }

        public RailPotentialItem(string station, RailPotentialState state)
        {
            StationName = station;
            RailPotentialState = state;
        }
    }

    public enum RailPotentialState
    {
        Close,
        Open,
        Fault
    }
}
