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
                case GitHubEventType.CommitCommentEvent:
                    PrintCommitCommentEvent(gitHubEvent);
                    break;
                case GitHubEventType.CreateEvent:
                    PrintCreateEvent(gitHubEvent);
                    break;
                case GitHubEventType.DeleteEvent:
                    PrintDeleteEvent(gitHubEvent);
                    break;
                case GitHubEventType.ForkEvent:
                    PrintForkEvent(gitHubEvent);
                    break;
                case GitHubEventType.GollumEvent:
                    PrintGollumEvent(gitHubEvent);
                    break;
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

    private static void PrintCommitCommentEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var commitCommentEvent = gitHubEvent.Payload.Deserialize<GitHubCommitCommentEventPayload>();
        if (commitCommentEvent is null) return;

        var commitIdLabel = commitCommentEvent.Comment.CommitId.Length > 7
            ? commitCommentEvent.Comment.CommitId[..7]
            : commitCommentEvent.Comment.CommitId;

        Console.WriteLine($"- Commented the commit '{commitIdLabel}' in '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintCreateEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var createEvent = gitHubEvent.Payload.Deserialize<GitHubCreateEventPayload>();
        if (createEvent is null) return;

        var message = createEvent.RefType switch
        {
            GitHubRefType.Branch or GitHubRefType.Tag => $"- {createEvent.RefType} '{createEvent.Ref}' created in '{gitHubEvent.Repository.Name}'",
            GitHubRefType.Repository => $"- {createEvent.RefType} ''{gitHubEvent.Repository.Name}'' created",
            _ => "Unknown create event action"
        };

        Console.WriteLine(message);
    }

    private static void PrintDeleteEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var deleteEvent = gitHubEvent.Payload.Deserialize<GitHubDeleteEventPayload>();
        if (deleteEvent is null) return;

        var message = deleteEvent.RefType switch
        {
            GitHubRefType.Branch or GitHubRefType.Tag => $"- {deleteEvent.RefType} '{deleteEvent.Ref}' deleted from '{gitHubEvent.Repository.Name}'",
            _ => "Unknown delete event action"
        };

        Console.WriteLine(message);
    }

    private static void PrintForkEvent(GitHubEvent gitHubEvent)
    {
        Console.WriteLine($"- Forked '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintGollumEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var golumEvent = gitHubEvent.Payload.Deserialize<GitHubGollumEventPayload>();
        if (golumEvent is null) return;

        var createdPages = golumEvent.Pages.Count(d => d.Action == GitHubWikiPageAction.Created);
        var editedPages = golumEvent.Pages.Count(d => d.Action == GitHubWikiPageAction.Edited);

        string prefixLabel = "Unknown wiki action";
        if (createdPages > 0 && editedPages > 0)
        {
            prefixLabel = $"Created {createdPages} and edited {editedPages} wiki pages";
        }
        else if (createdPages > 0)
        {
            var suffix = createdPages > 1 ? $"{createdPages} wiki pages" : "a wiki page";
            prefixLabel = $"Created {suffix}";
        }
        else if (editedPages > 0)
        {
            var suffix = editedPages > 1 ? $"{editedPages} wiki pages" : "a wiki page";
            prefixLabel = $"Edited {suffix}";
        }

        Console.WriteLine($"- {prefixLabel} into '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintPushEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var pushEvent = gitHubEvent.Payload.Deserialize<GitHubPushEventPayload>();
        if (pushEvent is null) return;

        var commitLabel = pushEvent.DistinctSize == 1 ? "commit" : "commits";
        Console.WriteLine($"- Pushed {pushEvent.DistinctSize} {commitLabel} to '{gitHubEvent.Repository.Name}'");
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

        Console.WriteLine($"- {prefixLabel} in '{gitHubEvent.Repository.Name}'");
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

        Console.WriteLine($"- {prefixLabel} '{gitHubEvent.Repository.Name}'");
    }
}