using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubCommitCommentEventPayload
{
    [JsonPropertyName("comment")]
    public required GitHubCommitComment Comment { get; init; }
}
