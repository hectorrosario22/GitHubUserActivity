using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Models;

namespace GitHubUserActivity.Services;

public class PrintService : IPrintService
{
    public void PrintEvents(List<GitHubEvent> events)
    {
        if (events.Count == 0)
        {
            Console.WriteLine("No events found.");
            return;
        }

        foreach (var gitHubEvent in events)
        {
            Console.WriteLine(gitHubEvent.ToString());
        }
    }
}