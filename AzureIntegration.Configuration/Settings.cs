using Microsoft.Extensions.Configuration;

namespace AzureIntegration.Configuration
{
    public class Settings
    {
        public string GenAIUrl { get; set; }
        public string GenAIApiKey { get; set; }
        public string GenAIEngine { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string ServiceBusQueueName { get; set; }
        public string ServiceBusTopicName { get; set; }
    }
}
