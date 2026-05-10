namespace wpf.ui.model.payloads
{
    public class Device
    {
        public int Id { get; set; }
        public required string DeviceCode { get; set; }
        public required string DeviceName { get; set; }
        public string? DisplayName { get; set; }
        public required string StationCode { get; set; }
        public string? StationName { get; set; }
        public int Type { get; set; }
        public string? DeviceState { get; set; }
        public bool PowerOn { get; set; }
    }
}
