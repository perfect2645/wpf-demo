using Logging;
using Messaging.LocalMessages.CircuitItem;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Utils.Enumerable;

namespace Messaging.LocalMessages.channel
{
    public class CircuitChannelService : IDataChannelService
    {
        private readonly ConcurrentDictionary<string, Channel<ICircuitItem>> _channels;

        public CircuitChannelService()
        {
            _channels = new ConcurrentDictionary<string, Channel<ICircuitItem>>();
        }

        public async Task SendDataAsync(ICircuitItem data)
        {
            if (data == null)
            {
                return;
            }

            TryAddChannel($"{data.StationId}{data.Id}");
            await _channels[$"{data.StationId}{data.Id}"].Writer.WriteAsync(data);
        }

        private void TryAddChannel(string id)
        {
            if (!_channels.Exists(id))
            {
                var options = new BoundedChannelOptions(20)
                {
                    FullMode = BoundedChannelFullMode.DropOldest
                };
                var channel = Channel.CreateBounded<ICircuitItem>(options);
                _channels.TryAdd(id, channel);
            }
        }

        public IAsyncEnumerable<ICircuitItem> SubscribeAsync(string id, CancellationToken? cancellationToken = null)
        {
            TryAddChannel(id);
            var cts = cancellationToken ?? CancellationToken.None;
            return GetConsumingEnumerable(id, cts);
        }

        private async IAsyncEnumerable<ICircuitItem> GetConsumingEnumerable(string id, CancellationToken? cancellationToken)
        {
            var cts = cancellationToken ?? CancellationToken.None;

            await foreach (var msg in _channels[id].Reader.ReadAllAsync(cts))
            {
                yield return msg;
            }
        }

        public void Remove(string id)
        {
            if (_channels.Exists(id))
            {
                _channels.RemoveKey(id);
            }
        }

        public void Log()
        {
            Log4Logger.Logger.Info($"{_channels.Count}");
        }
    }
}
