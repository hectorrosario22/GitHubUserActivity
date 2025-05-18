using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubPullRequestEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubPullRequestAction Action { get; init; }

    [JsonPropertyName("number")]
    public required long Number { get; init; }
}
