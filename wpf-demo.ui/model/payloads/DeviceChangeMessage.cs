namespace wpf.ui.model.payloads
{
    public class DeviceChangeMessage
    {
        public string? StationCode { get; set; }
        public string? DeviceCode { get; set; }
        public string? DeviceCategory { get; set; }
        public string? DeviceSate { get; set; }
        public bool? DevicePowerOn { get; set; }
        public string? SuccessMessage { get; set; }

        public string? TrackCode { get; set; }
        public string? TrackName { get; set; }
        public bool? TrackPowerOn { get; set; }

    }
}
