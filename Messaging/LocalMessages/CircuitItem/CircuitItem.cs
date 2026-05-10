using System.Diagnostics.CodeAnalysis;

namespace Messaging.LocalMessages.CircuitItem
{
    public class CircuitItem : ICircuitItem
    {
        public required string StationId { get; init; }

        public required string Id { get; init; }

        public string? StationName { get; set; }
        public string? Type { get; set; }

        public bool IsClose { get; set; }

        public bool PowerOn { get; set; }
        public string? DisplayName { get; set; }
        public string? DeviceName { get; set; }
        public string? Message { get; set; }

        [SetsRequiredMembers]
        public CircuitItem(string stationId, string id)
        {
            StationId = stationId;
            Id = id;
        }
    }
}
