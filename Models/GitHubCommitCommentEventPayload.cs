using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubCommitCommentEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubCommitCommentAction Action { get; init; }
}