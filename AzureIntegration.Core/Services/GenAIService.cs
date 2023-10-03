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
        private static string _lastAnswer;

        public GenAIService( Settings configuration)
        {
            _endpoint = configuration.GenAIUrl;
            _key = configuration.GenAIApiKey;
            _engine = configuration.GenAIEngine;
        }

        public async Task<string> GetAnswerToQuestion(string question)
        {
            OpenAIClient client = new(new Uri(_endpoint), new AzureKeyCredential(_key));            Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync(
                _engine,
                new ChatCompletionsOptions()
                {
                    Messages =
                    {
            new ChatMessage(ChatRole.System, @"You are an AI assistant that helps people find information that answers in tagalog"),
            new ChatMessage(ChatRole.User, question)
                    },
                    Temperature = (float)0.0,
                    MaxTokens = 50,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            ChatCompletions completions = responseWithoutStream.Value;
            _lastAnswer = completions.Choices.First().Message.Content;
            return _lastAnswer;
        }

        public async Task<string> GetLastAnswer()
        {
            return _lastAnswer;
        }
    }
}
