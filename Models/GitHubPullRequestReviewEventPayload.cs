using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequestReviewEventPayload
{
    [JsonPropertyName("pull_request")]
    public required GitHubPullRequest PullRequest { get; init; }
}
