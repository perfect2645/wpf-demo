namespace Messaging.LocalMessages.CircuitItem
{
    public interface ICircuitData
    {
        Dictionary<string, ICircuitItem> CircuitItems { get; }
        SemaphoreSlim CircuitItemsSemaphore { get; }
        void AddCircuitItem(ICircuitItem circuitItemVm);
    }
}
