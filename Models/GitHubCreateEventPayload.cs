using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubCreateEventPayload : IGitHubPayload
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }

    [JsonPropertyName("ref_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubRefType RefType { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        return RefType switch
        {
            GitHubRefType.Branch or GitHubRefType.Tag => $"Created {RefType.ToString().ToLower()} '{Ref}' in '{gitHubEvent.Repository.Name}'",
            GitHubRefType.Repository => $"Repository '{gitHubEvent.Repository.Name}' created",
            _ => "Unknown create event action"
        };
    }
}
