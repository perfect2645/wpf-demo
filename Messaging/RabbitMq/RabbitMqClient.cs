using Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;
using Utils;
using Utils.Configurations;

namespace Messaging.RabbitMq
{
    public class RabbitMqClient: IRabbitMqClient
    {
        public event Func<string, string, Task>? MessageReceived;
        public event Func<MessageEventArgs, Task>? MessageReceivedDetailed;

        private AsyncEventingBasicConsumer? _consumer;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly MqSettings _mqSettings;

        public RabbitMqClient(IOptions<MqSettings> mqSettings)
        {
            _mqSettings = mqSettings.Value;
            Log4Logger.Logger.Info(_mqSettings.HostName);
        }

        public async Task ConfigDirectModeAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "139.129.35.19",
                Port = 5672,
                UserName = "edu",
                Password = "edu68403",
                VirtualHost = "/"
            };

            try
            {
                _connection = await factory.CreateConnectionAsync("wpf client");
                _channel = await _connection.CreateChannelAsync();

                var exchange = "psc.device.exchange";

                await _channel.ExchangeDeclareAsync(
                    exchange: exchange,
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false,
                    arguments: null
);

                var routingKey = "device.change.*";

                var queue = await _channel.QueueDeclareAsync(
                    queue: "psc.device.change",
                    durable: true,
                    exclusive: false,
                    autoDelete: true,
                    arguments: null);

                await _channel.QueueBindAsync(
                    queue: queue.QueueName,
                    exchange: exchange,
                    routingKey: routingKey,
                    arguments: null);

                _consumer = new AsyncEventingBasicConsumer(_channel);
                SetupConsumer();
                var str = await _channel.BasicConsumeAsync(queue, false, _consumer);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"Error ConfigAsync MQ client: {ex.Message}");
            }
        }

        public async Task ConfigFanoutModeAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _mqSettings.HostName,
                Port = _mqSettings.Port,
                UserName = _mqSettings.UserName,
                Password = _mqSettings.Password,
                VirtualHost = _mqSettings.VirtualHost.NotNullString(),
            };

            try
            {
                _connection = await factory.CreateConnectionAsync("wpf client");
                _channel = await _connection.CreateChannelAsync();

                var exchange = _mqSettings.Exchange;

                await _channel.ExchangeDeclareAsync(
                    exchange: exchange,
                    type: ExchangeType.Fanout,
                    durable: true,
                    autoDelete: false,
                    arguments: null
                );

                var routingKey = _mqSettings.RoutingKey;
                var queueId = _mqSettings.QueueId;
                var processId = Process.GetCurrentProcess().Id;

                var queue = await _channel.QueueDeclareAsync(
                    queue: $"{queueId}-{ObjectExtension.GetTimeStamp()}-{processId}",
                    durable: true,
                    exclusive: false,
                    autoDelete: true,
                    arguments: null);

                await _channel.QueueBindAsync(
                    queue: $"{queue.QueueName}" ,
                    exchange: exchange,
                    routingKey: routingKey,
                    arguments: null);

                _consumer = new AsyncEventingBasicConsumer(_channel);
                SetupConsumer();
                var str = await _channel.BasicConsumeAsync(queue, false, _consumer);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"Error ConfigAsync MQ client: {ex.Message}");
            }
        }

        private void SetupConsumer()
        {
            _consumer!.ReceivedAsync += async (model, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = args.RoutingKey;

                    await OnMessageReceived(routingKey, message);

                    await _channel!.BasicAckAsync(args.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Log4Logger.Logger.Error($"Error processing MQ message: {ex.Message}");
                    //await _channel!.BasicNackAsync(args.DeliveryTag, multiple: false, requeue: true);
                }
            };

            _consumer.ShutdownAsync += async (sender, args) =>
            {
                Log4Logger.Logger.Warn("MQ consumer Shutdown.");
                await Task.CompletedTask;
            };
        }

        private async Task OnMessageReceived(string routingKey, string message)
        {
            if (MessageReceived != null)
            {
                await MessageReceived.Invoke(routingKey, message);
            }

            if (MessageReceivedDetailed != null)
            {
                var args = new MessageEventArgs
                {
                    RoutingKey = routingKey,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };
                await MessageReceivedDetailed.Invoke(args);
            }
        }
    }

    public class MessageEventArgs
    {
        public string? RoutingKey { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
