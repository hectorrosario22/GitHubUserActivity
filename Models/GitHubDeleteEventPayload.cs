using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubDeleteEventPayload
{
    [JsonPropertyName("ref")]
    public required string Ref { get; set; }

    [JsonPropertyName("ref_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubRefType RefType { get; init; }
}
