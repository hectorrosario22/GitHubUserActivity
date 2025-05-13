using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubPushEventPayload
{
    [JsonPropertyName("size")]
    public required int Size { get; init; }
    
    [JsonPropertyName("distinct_size")]
    public required int DistinctSize { get; init; }
}