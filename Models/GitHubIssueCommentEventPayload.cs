using System.Text;
using System.Text.Json.Serialization;

namespace GitHubUserActivity.Models;

public record GitHubIssueCommentEventPayload : IGitHubPayload
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GitHubIssueCommentAction Action { get; init; }

    [JsonPropertyName("issue")]
    public required GitHubIssue Issue { get; init; }

    public string ToString(GitHubEvent gitHubEvent)
    {
        StringBuilder sb = new();
        sb.Append(Action switch
        {
            GitHubIssueCommentAction.Created => "Created ",
            GitHubIssueCommentAction.Edited => "Edited ",
            GitHubIssueCommentAction.Deleted => "Deleted ",
            _ => string.Empty
        });
        sb.Append("a comment on ");

        if (Issue.PullRequest is null)
        {
            sb.Append($"issue #{Issue.Number}");
        }
        else
        {
            sb.Append($"pull request #{Issue.Number}");
        }

        sb.Append($" in '{gitHubEvent.Repository.Name}'");
        return sb.ToString();
    }
}
