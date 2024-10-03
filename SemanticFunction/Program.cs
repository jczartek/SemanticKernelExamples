using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini", configuration["apiKey"]!)
    .Build();

const string prompt = """
                      Translate the following text from {{$source_language}} to {{$target_language}}: "{{$text_to_translate}}"
                      """;

var translateFunction = kernel.CreateFunctionFromPrompt(prompt, new OpenAIPromptExecutionSettings()
{
    MaxTokens = 1000,
    Temperature = 0.5,
});

var result = await kernel.InvokeAsync(translateFunction, new KernelArguments
{
    ["source_language"] = "polish",
    ["target_language"] = "english",
    ["text_to_translate"] = "Pracuj tak ciężko jak to możliwe, to zwiększa szanse na sukces. Jeśli inni ludzie pracują 40 godzin w tygodniu, a ty 100 godzin, to ty w 4 miesiące osiągniesz to, co innym zajmie rok."
}) ;

Console.WriteLine(result);
Console.ReadLine();