using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini", configuration["apiKey"]!)
    .Build();

var path = Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "Prompts");
var prompts = kernel.CreatePluginFromPromptDirectory(path);

var result = await kernel.InvokeAsync(prompts["Translate"], new KernelArguments
{
    ["source_language"] = "polish",
    ["target_language"] = "english",
    ["text_to_translate"] = "Pracuj tak ciężko jak to możliwe, to zwiększa szanse na sukces. Jeśli inni ludzie pracują 40 godzin w tygodniu, a ty 100 godzin, to ty w 4 miesiące osiągniesz to, co innym zajmie rok."
}) ;

Console.WriteLine(result);
Console.ReadLine();