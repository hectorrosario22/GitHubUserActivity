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
                case GitHubEventType.IssuesEvent:
                    PrintIssueEvent(gitHubEvent);
                    break;
                case GitHubEventType.WatchEvent:
                    PrintWatchEvent(gitHubEvent);
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

        var commitLabel = pushEvent.DistinctSize == 1 ? "commit" : "commits";
        Console.WriteLine($"- Pushed {pushEvent.DistinctSize} {commitLabel} to {gitHubEvent.Repository.Name}");
    }

    private static void PrintIssueEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;
        
        var issueEvent = gitHubEvent.Payload.Deserialize<GitHubIssueEventPayload>();
        if (issueEvent is null) return;

        var prefixLabel = issueEvent.Action switch
        {
            GitHubIssueAction.Opened => "Opened a new issue",
            GitHubIssueAction.Edited => "Edited an issue",
            GitHubIssueAction.Closed => "Closed an issue",
            GitHubIssueAction.Reopened => "Reopened an issue",
            GitHubIssueAction.Assigned => "Assigned an issue",
            GitHubIssueAction.Unassigned => "Unassigned an issue",
            GitHubIssueAction.Labeled => "Labeled an issue",
            GitHubIssueAction.Unlabeled => "Unlabeled an issue",
            _ => "Unknown issue action"
        };

        Console.WriteLine($"- {prefixLabel} in {gitHubEvent.Repository.Name}");
    }

    private static void PrintWatchEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;
        
        var watchEvent = gitHubEvent.Payload.Deserialize<GitHubWatchEventPayload>();
        if (watchEvent is null) return;

        var prefixLabel = watchEvent.Action switch
        {
            GitHubWatchAction.Started => "Starred",
            _ => "Unknown watch action"
        };

        Console.WriteLine($"- {prefixLabel} {gitHubEvent.Repository.Name}");
    }
}