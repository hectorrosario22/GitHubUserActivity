using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = BuildServiceProvider();
var gitHubApiService = serviceProvider.GetRequiredService<IGitHubApiService>();
var printService = serviceProvider.GetRequiredService<IPrintService>();

string? username;
while (true)
{
    Console.Write("Enter GitHub username: ");
    username = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(username))
    {
        Console.WriteLine("Username cannot be empty.");
        continue;
    }

    break;
}

var eventsResult = await gitHubApiService.GetEvents(username);
if (!eventsResult.IsSuccess)
{
    Console.WriteLine(eventsResult.ErrorMessage);
    return;
}

printService.PrintEvents(eventsResult.Value!);

static ServiceProvider BuildServiceProvider()
{
    var services = new ServiceCollection();

    services.AddSingleton<IConfiguration>(provider =>
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    });

    services.AddHttpClient("GitHubApi", (sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var baseUrl = config["GitHubApi:BaseUrl"]
            ?? throw new InvalidOperationException("GitHub API BaseUrl not configured");

        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubUserActivity/1.0");
    });
    services.AddScoped<IGitHubApiService, GitHubApiService>();
    services.AddScoped<IPrintService, PrintService>();

    return services.BuildServiceProvider();
}