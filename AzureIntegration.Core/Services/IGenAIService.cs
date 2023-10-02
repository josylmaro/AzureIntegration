namespace AzureIntegration.Core.Services
{
    public interface IGenAIService
    {
        Task<string> GetAnswerToQuestion(string question);
    }
}
