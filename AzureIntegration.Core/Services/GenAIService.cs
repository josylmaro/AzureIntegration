using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using Azure;
using AzureIntegration.Configuration;
namespace AzureIntegration.Core.Services
{
    public class GenAIService :IGenAIService
    {
        private readonly string _endpoint;
        private readonly string _key;
        private readonly string _engine;

        public GenAIService( Settings configuration)
        {
            _endpoint = configuration.GenAIUrl;
            _key = configuration.GenAIApiKey;
            _engine = configuration.GenAIEngine;
        }

        public async Task<string> GetAnswerToQuestion(string question)
        {
            OpenAIClient client = new(new Uri(_endpoint), new AzureKeyCredential(_key));
            var response = await client.GetCompletionsAsync(_engine, question);
            string completion = response.Value.Choices[0].Text;
            return completion;
        }
    }
}
