using wpf.ui.controls.buttons;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace wpf.ui.model.diagrams.electric
{
    public class PowerOnOffItem
    {
        public required string BreakerId { get; init;}
        public required string BreakerName { get; init; }
        public BreakerState BreakerState1 { get; set; }
        public BreakerType BreakerType1 { get; set; }
        public BreakerState? BreakerState2 { get; set; }
        public BreakerType? BreakerType2 { get; set; }
        public string? Status { get; set; }
        public Brush Background1 { get; set; } = Brushes.Transparent;
        public Brush BorderBrush1 { get; set; } = Brushes.Yellow;
        public Brush Background2 { get; set; } = Brushes.Yellow;
        public Brush BorderBrush2 { get; set; } = Brushes.Transparent;

        [SetsRequiredMembers]
        public PowerOnOffItem(string breakerId, string breakerName, 
            BreakerState breakerState1, BreakerType breakerType1,
            BreakerState? breakerState2, BreakerType? breakerType2,
            string? status)
        {
            BreakerId = breakerId;
            BreakerName = breakerName;
            BreakerState1 = breakerState1;
            BreakerType1 = breakerType1;
            BreakerState2 = breakerState2;
            BreakerType2 = breakerType2;
            Status = status;

            InitColor();
        }

        private void InitColor()
        {
            if (BreakerState2 == null || BreakerType2 == null)
            {
                Background2 = Brushes.Transparent;
                BorderBrush2 = Brushes.Transparent;
            }
        }
    }
}
