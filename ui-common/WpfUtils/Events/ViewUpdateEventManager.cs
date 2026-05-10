using System.Runtime.InteropServices;
using System.Windows.Media;
using WpfUtils.Attributes;
using WpfUtils.Consts;

namespace UserComponent.Core.menu
{
    public static class ViewUpdateEventManager
    {
        #region ViewUpdateEvent

        private static event EventHandler<ViewUpdateEventArgs>? ViewUpdateEvent;

        public static void Subscribe(EventHandler<ViewUpdateEventArgs> handler)
        {
            ViewUpdateEvent += handler;
        }

        public static void UnSubscribe(EventHandler<ViewUpdateEventArgs> handler)
        {
            ViewUpdateEvent -= handler;
        }

        public static void Publish(object? sender, ViewUpdateEventArgs menuEventArgs)
        {
            ViewUpdateEvent?.Invoke(sender, menuEventArgs);
        }

        #endregion ViewUpdateEvent

        #region ViewUpdateEvent Async

        private static event Func<object?, ViewUpdateEventArgs, ValueTask>? ViewUpdateEventAsync;

        public static void SubscribeAsync(Func<object?, ViewUpdateEventArgs, ValueTask> handler)
        {
            ViewUpdateEventAsync += handler;
        }

        public static void UnSubscribeAsync(Func<object?, ViewUpdateEventArgs, ValueTask> handler)
        {
            ViewUpdateEventAsync -= handler;
        }

        public static async Task PublishAsync(object? sender, ViewUpdateEventArgs menuEventArgs)
        {
            if (ViewUpdateEventAsync == null)
            {
                return;
            }
            var invocationList = ViewUpdateEventAsync.GetInvocationList();
            var sortedInvocationList = new SortedList<int, Func<object?, ViewUpdateEventArgs, ValueTask>>();
            foreach (Func<object?, ViewUpdateEventArgs, ValueTask> handler in invocationList)
            {
                var orderAttribute = handler.Method.GetCustomAttributes(typeof(OrderAttribute), false).FirstOrDefault() as OrderAttribute;
                var order = orderAttribute?.Order ?? 999;
                sortedInvocationList.Add(order, handler);

            }

            foreach (var handler in sortedInvocationList.Values)
            {
                await handler(sender, menuEventArgs);
            }
        }

        public static async ValueTask PublishAsync(string menuId)
        {
            var args = new Dictionary<string, object>();
            args.Add(CommonConsts.ViewUpdateType, ViewUpdateType.MenuChange);
            args.Add(CommonConsts.MenuId, menuId);
            await PublishAsync(null, new ViewUpdateEventArgs(args!));
        }

        #endregion ViewUpdateEvent Async
    }

    public interface IViewUpdateEventArgs
    {
        IDictionary <string, object?> Parameters { get; }
    }

    public class ViewUpdateEventArgs : IViewUpdateEventArgs
    {
        public IDictionary<string, object?> Parameters { get; init; }

        public ViewUpdateEventArgs(IDictionary<string, object?> parameters)
        {
            Parameters = parameters;
        }

        public ViewUpdateEventArgs(string key, object? parameters)
        {
            ArgumentNullException.ThrowIfNull(key);

            Parameters = new Dictionary<string, object?>
            {
                {key, parameters }
            };
        }
    }
}
