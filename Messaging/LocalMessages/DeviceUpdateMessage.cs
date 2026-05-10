using Logging;
using Messaging.LocalMessages.CircuitItem;
using System.Diagnostics.CodeAnalysis;

namespace Messaging.LocalMessages
{
    public class DeviceUpdateMessage
    {
        public required WeakReference<ICircuitItem>? Sender { get; init; }

        [SetsRequiredMembers]
        public DeviceUpdateMessage(ICircuitItem sender)
        {
            if (sender == null)
            {
                Log4Logger.Logger.Warn($"Update device status failed. ICircuitItem is null.");
                return;
            }
            Sender = new WeakReference<ICircuitItem>(sender);
        }
    }
}
