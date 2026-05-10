namespace Messaging.RabbitMq
{
    public interface IRabbitMqClient
    {
        event Func<string, string, Task>? MessageReceived;
        event Func<MessageEventArgs, Task>? MessageReceivedDetailed;
        Task ConfigFanoutModeAsync();
        Task ConfigDirectModeAsync();
    }
}
