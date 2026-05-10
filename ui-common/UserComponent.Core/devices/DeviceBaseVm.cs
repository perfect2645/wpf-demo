using CommunityToolkit.Mvvm.ComponentModel;
using Messaging.LocalMessages;
using Messaging.LocalMessages.CircuitItem;

namespace UserComponent.Core.devices
{
    public partial class DeviceBaseVm : ObservableObject, ICircuitItem
    {
        public TransationMode TransationMode { get; set; } = TransationMode.Channel;

        #region ICircuitItem

        public required string StationId { get; init; }
        public required string Id { get; init; }
        public string? StationName { get; set; }
        public string? Type { get; set; }
        public bool IsClose { get; set; }
        public string? DisplayName { get; set; }
        public string? DeviceName { get; set; }
        public string? Message { get; set; }

        [ObservableProperty]
        private bool _powerOn;

        #endregion ICircuitItem
    }
}
