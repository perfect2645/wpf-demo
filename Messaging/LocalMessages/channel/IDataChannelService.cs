using Messaging.LocalMessages.CircuitItem;

namespace Messaging.LocalMessages.channel
{
    public interface IDataChannelService
    {
        Task SendDataAsync(ICircuitItem data);
        IAsyncEnumerable<ICircuitItem> SubscribeAsync(string id, CancellationToken? cancellationToken = null);
        void Remove(string id);
        void Log();
    }
}
