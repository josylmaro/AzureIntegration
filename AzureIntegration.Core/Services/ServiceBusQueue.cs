using Azure.Messaging.ServiceBus;
using Azure.Messaging;
using AzureIntegration.Configuration;

namespace AzureIntegration.Core.Services
{
    public class ServiceBusQueue : IServiceBusQueue
    {
        private readonly Settings _settings;
        public ServiceBusQueue(Settings settings)
        {
            _settings = settings;
        }
        public async Task<string> Queue(string message)
        {
            await using var client = new ServiceBusClient(_settings.ServiceBusConnectionString);
            ServiceBusSender sender = client.CreateSender(_settings.ServiceBusQueueName);
            var messageBody = new ServiceBusMessage(message);
            await sender.SendMessageAsync(messageBody);
            return messageBody.MessageId;
        }

        public async Task<string> Queue(string topic, string message)
        {
            await using var client = new ServiceBusClient(_settings.ServiceBusConnectionString);
            ServiceBusSender sender = client.CreateSender(_settings.ServiceBusTopicName);
            var response = new ServiceBusMessage(message);
            await sender.SendMessageAsync(response);
            return response.MessageId;
        }
    }   
}
