using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubGollumEventPayload : IGitHubPayload
{
    [JsonPropertyName("pages")]
    public required List<GitHubWikiPage> Pages { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        var createdCount = Pages.Count(page => page.Action == GitHubWikiPageAction.Created);
        var editedCount = Pages.Count(page => page.Action == GitHubWikiPageAction.Edited);

        string actionLabel = "Unknown wiki action";
        if (createdCount > 0 && editedCount > 0)
        {
            actionLabel = $"Created {createdCount} and edited {editedCount} wiki pages";
        }
        else if (createdCount > 0)
        {
            var suffix = createdCount > 1 ? $"{createdCount} wiki pages" : "a wiki page";
            actionLabel = $"Created {suffix}";
        }
        else if (editedCount > 0)
        {
            var suffix = editedCount > 1 ? $"{editedCount} wiki pages" : "a wiki page";
            actionLabel = $"Edited {suffix}";
        }

        return $"{actionLabel} into '{gitHubEvent.Repository.Name}'";
    }
}
