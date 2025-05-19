using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequestReviewEventPayload : IGitHubPayload
{
    [JsonPropertyName("pull_request")]
    public required GitHubPullRequest PullRequest { get; init; }

    public virtual string ToString(GitHubEvent gitHubEvent)
    {
        return $"Requested a review for pull request #{PullRequest.Number} in '{gitHubEvent.Repository.Name}'";
    }
}
