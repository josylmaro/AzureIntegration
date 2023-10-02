using AzureIntegration.Configuration;
using AzureIntegration.Core.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Gen AI API Service using OpenAI",
    Description = "Sends questions to Azure Open AI Service and returns a response",
    TermsOfService = new Uri("https://go.microsoft.com/fwlink/?LinkID=206977"),
    Contact = new OpenApiContact
    {
        Name = "Josyl Quiambao",
        Email = "josyl.m.b.quiambao",
        Url = new Uri("https://learn.microsoft.com/training")
    }
}));
builder.Services.AddSingleton(builder.Configuration.GetSection("AppSettings").Get<Settings>());


builder.Services.AddScoped<IGenAIService, GenAIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Integration Sample Use Case");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
