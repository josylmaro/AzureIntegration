using Azure.Storage.Queues.Models;
using AzureIntegration.FunctionApp.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureIntegration.FunctionApp
{
    public class SendEmailFunction
    {
        private readonly ILogger<SendEmailFunction> _logger;
        private readonly HttpClient _httpClient;
        private const string _logicAppUrl = "https://prod-38.southeastasia.logic.azure.com:443/workflows/ff50383d1b564d28979de717ddced6d8/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=xFq2CFLbBPgSIcvpTcJQjHXz0zJTy0N_MFjbnOe1rN4\r\n       ";

        public SendEmailFunction(ILogger<SendEmailFunction> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        [Function(nameof(SendEmailFunction))]
        public async Task Run([QueueTrigger("azureintegration", Connection = "AzureIntegrationServiceBusConnection")] QueueMessage message)
        {
            var request = new LogicAppSendEmail()
            {
                Message = message.Body.ToString()
            };
            var requestBody = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_logicAppUrl, requestBody);

            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
