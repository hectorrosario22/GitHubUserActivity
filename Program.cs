using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Services;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = BuildServiceProvider();
var gitHubApiService = serviceProvider.GetRequiredService<IGitHubApiService>();
var printService = serviceProvider.GetRequiredService<IPrintService>();

var events = await gitHubApiService.GetEvents("hectorrosario22");
printService.PrintEvents(events);

static ServiceProvider BuildServiceProvider()
{
    var services = new ServiceCollection();

    services.AddHttpClient("GitHubApi", client =>
    {
        client.BaseAddress = new Uri("https://api.github.com/"); // TODO: Move to config
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    });
    services.AddScoped<IGitHubApiService, GitHubApiService>();
    services.AddScoped<IPrintService, PrintService>();

    return services.BuildServiceProvider();
}