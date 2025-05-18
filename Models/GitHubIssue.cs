using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubIssue
{
    [JsonPropertyName("number")]
    public required int Number { get; init; }
    
    [JsonPropertyName("pull_request")]
    public object? PullRequest { get; init; }
}
