using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubCreateEventPayload
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }

    [JsonPropertyName("ref_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubRefType RefType { get; init; }
}
