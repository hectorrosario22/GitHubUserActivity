using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubEvent
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubEventType Type { get; init; }

    [JsonPropertyName("repo")]
    public required GitHubEventRepo Repository { get; init; }

    [JsonPropertyName("payload")]
    public JsonDocument? Payload { get; init; }
}
