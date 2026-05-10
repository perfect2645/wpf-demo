using Ioc;
using Microsoft.Extensions.DependencyInjection;
using UserComponent.Core.route;

namespace UserComponent.Core.menu
{
    public static class MenuEventManager
    {
        #region MenuSelectedEvent
        private static event EventHandler<MenuEventArgs>? MenuEvent;

        public static void Subscribe(EventHandler<MenuEventArgs> handler)
        {
            MenuEvent += handler;
        }

        public static void UnSubscribe(EventHandler<MenuEventArgs> handler)
        {
            MenuEvent -= handler;
        }

        public static void Publish(object? sender, MenuEventArgs menuEventArgs)
        {
            MenuEvent?.Invoke(sender, menuEventArgs);
        }

        #endregion MenuSelectedEvent
    }

    public class MenuEventArgs
    {
        public MenuButtonBaseVm? SelectedMenu { get; }
        public MenuEventArgs(MenuButtonBaseVm? selectedMenu)
        {
            SelectedMenu = selectedMenu;
        }
    }
}
