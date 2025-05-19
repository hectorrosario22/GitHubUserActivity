using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubIssueEventPayload : IGitHubPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubIssueAction Action { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        var actionLabel = Action switch
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

        return $"{actionLabel} in '{gitHubEvent.Repository.Name}'";
    }
}
