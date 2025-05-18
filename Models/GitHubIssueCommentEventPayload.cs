using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubIssueCommentEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubIssueCommentAction Action { get; init; }

    [JsonPropertyName("issue")]
    public required GitHubIssue Issue { get; init; }
}
