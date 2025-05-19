using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubCommitCommentEventPayload : IGitHubPayload
{
    [JsonPropertyName("comment")]
    public required GitHubCommitComment Comment { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        var commitIdLabel = Comment.CommitId.Length > 7
            ? Comment.CommitId[..7]
            : Comment.CommitId;

        return $"Commented the commit '{commitIdLabel}' in '{gitHubEvent.Repository.Name}'";
    }
}
