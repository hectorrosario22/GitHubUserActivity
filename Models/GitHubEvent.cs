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
    public object? Payload { get; init; } // TODO: Define a specific type for Payload based on the event type
}
