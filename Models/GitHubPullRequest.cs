using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequest
{
    [JsonPropertyName("number")]
    public long Number { get; init; }
}
