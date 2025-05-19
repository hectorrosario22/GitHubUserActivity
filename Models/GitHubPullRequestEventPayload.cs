using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequestEventPayload : IGitHubPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubPullRequestAction Action { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        var actionLabel = Action switch
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

        return $"{actionLabel} in '{gitHubEvent.Repository.Name}'";
    }
}
