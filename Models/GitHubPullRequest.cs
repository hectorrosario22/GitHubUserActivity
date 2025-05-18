using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequest
{
    [JsonPropertyName("number")]
    public required long Number { get; init; }
}
