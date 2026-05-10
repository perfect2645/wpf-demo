using System.Windows;
using Utils;

namespace wpf.ui.controls.buttons
{
    public class DiagramBreaker : BreakerButton
    {
        private static readonly Type ControlType = typeof(DiagramBreaker);

        public string StationId
        {
            get { return (string)GetValue(StationIdProperty); }
            set { SetValue(StationIdProperty, value); }
        }

        public static readonly DependencyProperty StationIdProperty =
            DependencyProperty.Register("StationId", typeof(string), ControlType);

        public string BreakerId
        {
            get { return (string)GetValue(BreakerIdProperty); }
            set { SetValue(BreakerIdProperty, value); }
        }

        public static readonly DependencyProperty BreakerIdProperty =
            DependencyProperty.Register("BreakerId", typeof(string), ControlType);

        public DiagramBreaker()
        {
            Loaded += DiagramBreaker_Loaded;
            Unloaded += DiagramBreaker_Unloaded;
        }

        protected override void OnInitialized(EventArgs e)
        {
            if (string.IsNullOrEmpty(StationId))
            {
                throw new ArgumentNullException($"Breaker:[{BreakerId}] - StationId is empty.");
            }
            if (string.IsNullOrEmpty(BreakerId))
            {
                throw new ArgumentNullException($"Station:[{StationId}] - BreakerId is empty.");
            }

            if (DataContext == null || DataContext is not BreakerButtonViewModel)
            {
                var vm = new BreakerButtonViewModel(StationId, BreakerId);
                DataContext = vm;
            }

            base.OnInitialized(e);
        }

        private void DiagramBreaker_Loaded(object? sender, EventArgs e)
        {
            (DataContext as BreakerButtonViewModel)!.OnLoaded();
        }

        private void DiagramBreaker_Unloaded(object sender, RoutedEventArgs e)
        {
            (DataContext as BreakerButtonViewModel)?.Dispose();
        }
    }
}
