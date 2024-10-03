using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini", configuration["apiKey"]!)
    .Build();
    
const string prompt = "What is Semantic Kernel? Describe it in two sentences.";
var promptAsync = await kernel.InvokePromptAsync(prompt);

Console.WriteLine(promptAsync);
Console.ReadLine();