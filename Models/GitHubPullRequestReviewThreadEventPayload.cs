using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequestReviewThreadEventPayload : GitHubPullRequestReviewEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubThreadAction Action { get; init; }

    public override string ToString(GitHubEvent gitHubEvent)
    {
        var actionLabel = Action switch
        {
            GitHubThreadAction.Resolved => "Resolved a review thread",
            GitHubThreadAction.Unresolved => "Unresolved a review thread",
            _ => "Unknown review thread action"
        };

        return $"{actionLabel} for pull request #{PullRequest.Number} in '{gitHubEvent.Repository.Name}'";
    }
}
