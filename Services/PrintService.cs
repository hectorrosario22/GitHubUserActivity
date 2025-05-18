using System.Text;
using System.Text.Json;
using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Models;

namespace GitHubUserActivity.Services;

public class PrintService : IPrintService
{
    public void PrintEvents(List<GitHubEvent> events)
    {
        if (events is null || events.Count == 0)
        {
            Console.WriteLine("No events found.");
            return;
        }

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
                case GitHubEventType.IssueCommentEvent:
                    PrintIssueCommentEvent(gitHubEvent);
                    break;
                case GitHubEventType.IssuesEvent:
                    PrintIssueEvent(gitHubEvent);
                    break;
                case GitHubEventType.MemberEvent:
                    PrintMemberEvent(gitHubEvent);
                    break;
                case GitHubEventType.PublicEvent:
                    PrintPublicEvent(gitHubEvent);
                    break;
                case GitHubEventType.PullRequestEvent:
                    PrintPullRequestEvent(gitHubEvent);
                    break;
                case GitHubEventType.PullRequestReviewEvent:
                    PrintPullRequestReviewEvent(gitHubEvent);
                    break;
                case GitHubEventType.PullRequestReviewCommentEvent:
                    PrintPullRequestReviewCommentEvent(gitHubEvent);
                    break;
                case GitHubEventType.PushEvent:
                    PrintPushEvent(gitHubEvent);
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
            GitHubRefType.Repository => $"- {createEvent.RefType} '{gitHubEvent.Repository.Name}' created",
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

        var createdCount = golumEvent.Pages.Count(page => page.Action == GitHubWikiPageAction.Created);
        var editedCount = golumEvent.Pages.Count(page => page.Action == GitHubWikiPageAction.Edited);

        string actionLabel = "Unknown wiki action";
        if (createdCount > 0 && editedCount > 0)
        {
            actionLabel = $"Created {createdCount} and edited {editedCount} wiki pages";
        }
        else if (createdCount > 0)
        {
            var suffix = createdCount > 1 ? $"{createdCount} wiki pages" : "a wiki page";
            actionLabel = $"Created {suffix}";
        }
        else if (editedCount > 0)
        {
            var suffix = editedCount > 1 ? $"{editedCount} wiki pages" : "a wiki page";
            actionLabel = $"Edited {suffix}";
        }

        Console.WriteLine($"- {actionLabel} into '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintIssueCommentEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var issueCommentEvent = gitHubEvent.Payload.Deserialize<GitHubIssueCommentEventPayload>();
        if (issueCommentEvent is null) return;

        StringBuilder sb = new("- ");
        sb.Append(issueCommentEvent.Action switch
        {
            GitHubIssueCommentAction.Created => "Created ",
            GitHubIssueCommentAction.Edited => "Edited ",
            GitHubIssueCommentAction.Deleted => "Deleted ",
            _ => string.Empty
        });
        sb.Append("a comment on ");

        if (issueCommentEvent.Issue.PullRequest is null)
        {
            sb.Append($"issue #{issueCommentEvent.Issue.Number}");
        }
        else
        {
            sb.Append($"pull request #{issueCommentEvent.Issue.Number}");
        }

        sb.Append($" in '{gitHubEvent.Repository.Name}'");
        Console.WriteLine(sb.ToString());
    }

    private static void PrintIssueEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var issueEvent = gitHubEvent.Payload.Deserialize<GitHubIssueEventPayload>();
        if (issueEvent is null) return;

        var actionLabel = issueEvent.Action switch
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

        Console.WriteLine($"- {actionLabel} in '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintMemberEvent(GitHubEvent gitHubEvent)
    {
        Console.WriteLine($"- Added a collaborator in '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintPublicEvent(GitHubEvent gitHubEvent)
    {
        Console.WriteLine($"- Made '{gitHubEvent.Repository.Name}' public");
    }

    private static void PrintPushEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var pushEvent = gitHubEvent.Payload.Deserialize<GitHubPushEventPayload>();
        if (pushEvent is null) return;

        var commitLabel = pushEvent.Size == 1 ? "commit" : "commits";
        Console.WriteLine($"- Pushed {pushEvent.Size} {commitLabel} to '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintPullRequestEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var pullRequestEvent = gitHubEvent.Payload.Deserialize<GitHubPullRequestEventPayload>();
        if (pullRequestEvent is null) return;

        var actionLabel = pullRequestEvent.Action switch
        {
            GitHubPullRequestAction.Opened => "Opened a pull request",
            GitHubPullRequestAction.Edited => "Edited a pull request",
            GitHubPullRequestAction.Closed => "Closed a pull request",
            GitHubPullRequestAction.Reopened => "Reopened a pull request",
            GitHubPullRequestAction.Assigned => "Assigned a pull request",
            GitHubPullRequestAction.Unassigned => "Unassigned a pull request",
            GitHubPullRequestAction.ReviewRequested => "Requested a review for a pull request",
            GitHubPullRequestAction.ReviewRequestRemoved => "Removed a review request for a pull request",
            GitHubPullRequestAction.Labeled => "Labeled a pull request",
            GitHubPullRequestAction.Unlabeled => "Unlabeled a pull request",
            GitHubPullRequestAction.Synchronize => "Synchronized a pull request",
            _ => "Unknown pull request action"
        };

        Console.WriteLine($"- {actionLabel} in '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintPullRequestReviewEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var pullRequestReviewEvent = gitHubEvent.Payload.Deserialize<GitHubPullRequestReviewEventPayload>();
        if (pullRequestReviewEvent is null) return;

        Console.WriteLine($"- Requested a review for pull request #{pullRequestReviewEvent.PullRequest.Number} in '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintPullRequestReviewCommentEvent(GitHubEvent gitHubEvent)
    {
        if (gitHubEvent.Payload is null) return;

        var pullRequestReviewCommentEvent = gitHubEvent.Payload.Deserialize<GitHubPullRequestReviewCommentEventPayload>();
        if (pullRequestReviewCommentEvent is null) return;

        Console.WriteLine($"- Commented on pull request #{pullRequestReviewCommentEvent.PullRequest.Number} in '{gitHubEvent.Repository.Name}'");
    }

    private static void PrintWatchEvent(GitHubEvent gitHubEvent)
    {
        Console.WriteLine($"- Starred '{gitHubEvent.Repository.Name}'");
    }
}