using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubIssue
{
    [JsonPropertyName("number")]
    public required long Number { get; init; }

    [JsonPropertyName("pull_request")]
    public GitHubPullRequest? PullRequest { get; init; }
}
