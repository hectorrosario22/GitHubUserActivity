using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubPushEventPayload
{
    [JsonPropertyName("distinct_size")]
    public required int DistinctSize { get; init; }
}
