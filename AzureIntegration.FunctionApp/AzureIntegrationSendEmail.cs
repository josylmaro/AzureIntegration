using System.Net;
using AzureIntegration.FunctionApp.Model;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace AzureIntegration.FunctionApp
{
    public class AzureIntegrationSendEmail
    {
        private readonly ILogger _logger;
        private const string _logicAppUrl = "https://prod-38.southeastasia.logic.azure.com:443/workflows/ff50383d1b564d28979de717ddced6d8/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=xFq2CFLbBPgSIcvpTcJQjHXz0zJTy0N_MFjbnOe1rN4\r\n       ";


        public AzureIntegrationSendEmail(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AzureIntegrationSendEmail>();
        }

        [Function("AzureIntegrationSendEmail")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            using (var httpClient = new HttpClient())
            {
                string content = await new StreamReader(req.Body).ReadToEndAsync();
                var body = new LogicAppSendEmail()
                {
                    Message = content
                };
                var requestBody = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var httpResponseMessage = await httpClient.PostAsync(_logicAppUrl, requestBody);

                _logger.LogInformation($"C# http trigger function processed: {req}");

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                response.WriteString($"Received message \"{content}\" and sent to logic app for email sending ");

                return response;
            }
        }
    }
}
