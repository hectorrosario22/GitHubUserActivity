// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Services;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = BuildServiceProvider();
var gitHubApiService = serviceProvider.GetRequiredService<IGitHubApiService>();

var events = await gitHubApiService.GetEvents("hectorrosario22");
Console.WriteLine(JsonSerializer.Serialize(events, new JsonSerializerOptions
{
    WriteIndented = true
}));

static ServiceProvider BuildServiceProvider()
{
    var services = new ServiceCollection();

    services.AddHttpClient("GitHubApi", client =>
    {
        client.BaseAddress = new Uri("https://api.github.com/"); // TODO: Move to config
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    });
    services.AddScoped<IGitHubApiService, GitHubApiService>();

    return services.BuildServiceProvider();
}