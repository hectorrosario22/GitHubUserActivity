using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubPushEventPayload
{
    [JsonPropertyName("size")]
    public required int Size { get; init; }
}
