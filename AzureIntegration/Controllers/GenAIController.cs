using AzureIntegration.Core.Models;
using AzureIntegration.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureIntegration.Controllers;

[ApiController]
[Route("[controller]")]
public class GenAIController : ControllerBase
{

    private readonly ILogger<GenAIController> _logger;
    private readonly IGenAIService _genAIService;

    public GenAIController(ILogger<GenAIController> logger, IGenAIService genAIService)
    {
        _logger = logger;
        _genAIService = genAIService;
    }

    //create a post method to get the answer from the question
    [HttpPost]
    public async Task<string> Post([FromBody] string question)
    {
        return await _genAIService.GetAnswerToQuestion(question);
    }
}
