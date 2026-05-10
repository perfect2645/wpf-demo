namespace wpf.ui.model.payloads
{
    public class TrackNetAsset
    {
        public required List<Track> TrackList { get; set; }
        public required List<Device> DeviceList { get; set; }
        public required List<Bus> BusList { get; set; }
    }
}
