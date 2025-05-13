using System.Text.Json;
using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Models;

namespace GitHubUserActivity.Services;

public class PrintService : IPrintService
{
    public void PrintEvents(List<GitHubEvent> events)
    {
        foreach (var gitHubEvent in events)
        {
            switch (gitHubEvent.Type)
            {
                case GitHubEventType.PushEvent:
                    PrintPushEvent(gitHubEvent);
                    break;
                default:
                    Console.WriteLine("- Not implemented event type");
                    break;
            }
        }
    }

    private static void PrintPushEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;
        
        var pushEvent = gitHubEvent.Payload.Deserialize<GitHubPushEventPayload>();
        if (pushEvent is null) return;

        var commitLabel = pushEvent.Size == 1 ? "commit" : "commits";
        Console.WriteLine($"- Pushed {pushEvent.DistinctSize} {commitLabel} to {gitHubEvent.Repository.Name}");
    }
}