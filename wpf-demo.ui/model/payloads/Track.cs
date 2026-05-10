namespace wpf.ui.model.payloads
{
    public class Track
    {
        public required string TrackCode { get; set; }
        public string? TrackName { get; set; }
        public string? DisplayName { get; set; }
        public int Direction { get; set; }
        public required string StationCode { get; set; }
        public bool PowerOn { get; set; }
    }
}
