using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubEvent
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubEventType Type { get; init; }

    [JsonPropertyName("repo")]
    public required GitHubEventRepo Repository { get; init; }

    [JsonPropertyName("payload")]
    public JsonDocument? Payload { get; init; }

    public override string ToString()
    {
        return Type switch
        {
            GitHubEventType.CommitCommentEvent => $"- {Payload?.Deserialize<GitHubCommitCommentEventPayload>()?.ToString(this)}",
            GitHubEventType.CreateEvent => $"- {Payload?.Deserialize<GitHubCreateEventPayload>()?.ToString(this)}",
            GitHubEventType.DeleteEvent => $"- {Payload?.Deserialize<GitHubDeleteEventPayload>()?.ToString(this)}",
            GitHubEventType.ForkEvent => $"- Forked the repository '{Repository.Name}'",
            GitHubEventType.GollumEvent => $"- {Payload?.Deserialize<GitHubGollumEventPayload>()?.ToString(this)}",
            GitHubEventType.IssueCommentEvent => $"- {Payload?.Deserialize<GitHubIssueCommentEventPayload>()?.ToString(this)}",
            GitHubEventType.IssuesEvent => $"- {Payload?.Deserialize<GitHubIssueEventPayload>()?.ToString(this)}",
            GitHubEventType.MemberEvent => $"- Added a collaborator in '{Repository.Name}'",
            GitHubEventType.PublicEvent => $"- Made '{Repository.Name}' public",
            GitHubEventType.PullRequestEvent => $"- {Payload?.Deserialize<GitHubPullRequestEventPayload>()?.ToString(this)}",
            GitHubEventType.PullRequestReviewEvent => $"- {Payload?.Deserialize<GitHubPullRequestReviewEventPayload>()?.ToString(this)}",
            GitHubEventType.PullRequestReviewCommentEvent => $"- {Payload?.Deserialize<GitHubPullRequestReviewCommentEventPayload>()?.ToString(this)}",
            GitHubEventType.PullRequestReviewThreadEvent => $"- {Payload?.Deserialize<GitHubPullRequestReviewThreadEventPayload>()?.ToString(this)}",
            GitHubEventType.PushEvent => $"- {Payload?.Deserialize<GitHubPushEventPayload>()?.ToString(this)}",
            GitHubEventType.ReleaseEvent => $"- Released a new version in '{Repository.Name}'",
            GitHubEventType.SponsorshipEvent => "- Received a sponsorship",
            GitHubEventType.WatchEvent => $"- Starred '{Repository.Name}'",
            _ => $"- Unknown event '{Type}' in '{Repository.Name}'",
        };
    }
}
