using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubIssueEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubIssueAction Action { get; init; }
}