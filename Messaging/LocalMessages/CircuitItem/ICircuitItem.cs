namespace Messaging.LocalMessages.CircuitItem
{
    public interface ICircuitItem
    {
        string StationId { get; }
        string Id { get; }
        string? StationName { get; }
        string? Type { get; }
        bool IsClose { get; }
        bool PowerOn { get; }
        string? DisplayName { get; }
        string? DeviceName { get; }
        string? Message { get; }
    }
}
