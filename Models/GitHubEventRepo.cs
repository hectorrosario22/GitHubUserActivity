using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubEventRepo
{
    [JsonPropertyName("id")]
    public required long Id { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
