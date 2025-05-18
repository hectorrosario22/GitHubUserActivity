using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubPullRequestReviewThreadEventPayload : GitHubPullRequestReviewEventPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubThreadAction Action { get; init; }
}
