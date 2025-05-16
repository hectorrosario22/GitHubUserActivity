using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubGollumEventPayload
{
    [JsonPropertyName("pages")]
    public required List<GitHubWikiPage> Pages { get; init; }
}
