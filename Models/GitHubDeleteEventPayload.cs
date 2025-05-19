using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubDeleteEventPayload : IGitHubPayload
{
    [JsonPropertyName("ref")]
    public required string Ref { get; set; }

    [JsonPropertyName("ref_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubRefType RefType { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        return RefType switch
        {
            GitHubRefType.Branch or GitHubRefType.Tag => $"{RefType} '{Ref}' deleted from '{gitHubEvent.Repository.Name}'",
            _ => "Unknown delete event action"
        };
    }
}
