namespace Messaging.RabbitMq
{
    public class MqSettings
    {
        public required string HostName { get; set; }
        public required int Port { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? VirtualHost { get; set; } = "/";
        public required string Exchange { get; set; }
        public required string RoutingKey { get; set; }
        public required string QueueId { get; set; }
    }
}
