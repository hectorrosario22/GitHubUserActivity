using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPushEventPayload : IGitHubPayload
{
    [JsonPropertyName("size")]
    public required int Size { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        var commitLabel = Size == 1 ? "commit" : "commits";
        return $"Pushed {Size} {commitLabel} to '{gitHubEvent.Repository.Name}'";
    }
}
