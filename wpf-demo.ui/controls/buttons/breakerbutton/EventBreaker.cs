using System.Windows;

namespace wpf.ui.controls.buttons
{
    public class EventBreaker : BreakerButton
    {
        private static readonly Type ControlType = typeof(EventBreaker);

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

        public EventBreaker()
        {
            Loaded += EventBreaker_Loaded;
            Unloaded += EventBreaker_Unloaded;
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

            if (DataContext == null || DataContext is not EventBreakerViewModel)
            {
                var vm = new EventBreakerViewModel(StationId, BreakerId);
                DataContext = vm;
            }

            base.OnInitialized(e);
        }

        private void EventBreaker_Loaded(object? sender, EventArgs e)
        {
            (DataContext as EventBreakerViewModel)!.OnLoaded();
        }

        private void EventBreaker_Unloaded(object sender, RoutedEventArgs e)
        {
            (DataContext as EventBreakerViewModel)?.Dispose();
        }
    }
}
