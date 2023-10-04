using Newtonsoft.Json;

namespace AzureIntegration.FunctionApp.Model
{
    public class LogicAppSendEmail
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
