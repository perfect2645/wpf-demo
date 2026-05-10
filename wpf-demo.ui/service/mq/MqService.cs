using wpf.ui.constants;
using wpf.ui.model.payloads;
using Logging;
using Messaging.LocalMessages.channel;
using Messaging.LocalMessages.CircuitItem;
using Messaging.LocalMessages.events;
using Messaging.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Utils;
using Utils.Object;
using Utils.Tasking;
using WpfUtils.Consts;

namespace wpf.ui.service.mq
{
    public class MqService
    {
        private IRabbitMqClient? _rabbitMqClient;
        private IDataChannelService _dataChannelService;
        public MqService(IRabbitMqClient rabbitMqClient,
            [FromKeyedServices(CommonConsts.Ioc_CircuitChannel)] IDataChannelService dataChannelService)
        {
            _rabbitMqClient = rabbitMqClient;
            _rabbitMqClient.ConfigFanoutModeAsync().SafeFireAndForget(OnSuccess, OnError);
            _dataChannelService = dataChannelService;
        }

        public void RegisterMq(Func<string, string, Task> msgAction)
        {
            _rabbitMqClient!.MessageReceived += msgAction;
        }

        public void UnRegisterMq(Func<string, string, Task> msgAction)
        {
            _rabbitMqClient!.MessageReceived -= msgAction;
        }

        private void OnError(Exception exception)
        {
        }

        private void OnSuccess()
        {
            _rabbitMqClient!.MessageReceived += MessageReceived;
        }

        private async Task MessageReceived(string routingKey, string message)
        {
            Log4Logger.Logger.Debug($"Mq message received:[{message}].");
            if (string.IsNullOrEmpty(message))
            {
                Log4Logger.Logger.Warn($"Mq message is empty.routingKey:{routingKey}.");
                return;
            }

            try
            {
                var device = JsonSerializer.Deserialize<DeviceChangeMessage>(message, JsonEncoder.JsonOption);
                if (device == null)
                {
                    Log4Logger.Logger.Warn($"Mq message deserialization failed.message:{message}.");
                    return;
                }
                await PublishCircuitMessage(device);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"MQ - Deserialize<DeviceChangeMessage> failed.[{message}]", ex);
            }
        }

        private async Task PublishCircuitMessage(DeviceChangeMessage device)
        {
            var item = device.DeviceCategory switch
            {
                nameof(DeviceCategory.TR) => new CircuitItem(device.StationCode.NotNullString(), device.TrackCode!)
                {
                    PowerOn = device.TrackPowerOn.NotNullBool(),
                    Message = device.SuccessMessage.NotNullString()
                },
                _ => new CircuitItem(device.StationCode.NotNullString(), device.DeviceCode!)
                {
                    IsClose = device.DevicePowerOn.NotNullBool(),
                    PowerOn = device.DevicePowerOn.NotNullBool(),
                }
            };

            await _dataChannelService.SendDataAsync(item).ConfigureAwait(false);
            await CircuitEventManager.PublishAsync(item).ConfigureAwait(false);
        }
    }
}
