using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubCommitComment
{
    [JsonPropertyName("commit_id")]
    public required string CommitId { get; init; }
}