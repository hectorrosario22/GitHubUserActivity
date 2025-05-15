using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubWatchEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubWatchAction Action { get; init; }
}