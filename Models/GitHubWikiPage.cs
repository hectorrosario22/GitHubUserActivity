using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public class GitHubWikiPage
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubWikiPageAction Action { get; init; }
}
