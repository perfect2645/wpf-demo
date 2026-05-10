namespace wpf.ui.model.payloads
{
    public class StationCircuitData
    {
        public required Station Station { get; set; }
        public List<Device>? DeviceList { get; set; }
        public List<Bus>? BusList { get; set; }
    }
}
