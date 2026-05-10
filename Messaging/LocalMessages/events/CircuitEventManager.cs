using Messaging.LocalMessages.CircuitItem;
using System;
using System.Diagnostics.Tracing;

namespace Messaging.LocalMessages.events
{
    public class CircuitEventManager
    {
        private static event Func<object?, CircuitEventArgs, ValueTask>? CircuitUpdateEventAsync;

        public static void SubscribeAsync(Func<object?, CircuitEventArgs, ValueTask> handler)
        {
            CircuitUpdateEventAsync += handler;
        }

        public static void UnSubscribeAsync(Func<object?, CircuitEventArgs, ValueTask> handler)
        {
            CircuitUpdateEventAsync -= handler;
        }

        public static async Task PublishAsync(object? sender, CircuitEventArgs eventArgs)
        {
            if (CircuitUpdateEventAsync == null)
            {
                return;
            }
            var invocationList = CircuitUpdateEventAsync.GetInvocationList();
            foreach (Func<object?, CircuitEventArgs, ValueTask> handler in invocationList)
            {
                await handler(sender, eventArgs).ConfigureAwait(false);
            }
        }

        public static async Task PublishAsync(ICircuitItem circuitItem)
        {
            if (CircuitUpdateEventAsync == null)
            {
                return;
            }
            var circuitEventArgs = new CircuitEventArgs
            {
                CircuitItem = circuitItem
            };
            var invocationList = CircuitUpdateEventAsync.GetInvocationList();
            foreach (Func<object?, CircuitEventArgs, ValueTask> handler in invocationList)
            {
                await handler(null, circuitEventArgs).ConfigureAwait(false);
            }
        }
    }

    public class CircuitEventArgs : EventArgs
    {
        public required ICircuitItem CircuitItem { get; set; }
    }
}
