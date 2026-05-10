namespace UserComponent.Core.station
{
    public static class StationEventManager
    {
        #region StationSelectedEvent
        private static event EventHandler<StationEventArgs>? StationEvent;

        public static void Subscribe(EventHandler<StationEventArgs> handler)
        {
            StationEvent += handler;
        }

        public static void UnSubscribe(EventHandler<StationEventArgs> handler)
        {
            if (StationEvent == null) return;
            StationEvent -= handler;
        }

        public static void Publish(object? sender, StationEventArgs StationEventArgs)
        {
            StationEvent?.Invoke(sender, StationEventArgs);
        }

        #endregion StationSelectedEvent
    }

    public class StationEventArgs
    {
        public StationBaseVm? SelectedStation { get; }
        public StationEventArgs(StationBaseVm? selectedStation)
        {
            SelectedStation = selectedStation;
        }
    }
}
