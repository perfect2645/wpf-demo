namespace wpf.ui.model.payloads
{
    public class Bus
    {
        public required string BusCode { get; set; }
        public string? BusName { get; set; }
        public required string StationCode { get; set; }
        public string? StationName { get; set; }
        public string? Type { get; set; }
        public bool PowerOn { get; set; }
    }
}
