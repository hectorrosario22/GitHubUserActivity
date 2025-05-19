namespace GitHubUserActivity.Models;

public record GitHubPullRequestReviewCommentEventPayload : GitHubPullRequestReviewEventPayload
{
    public override string ToString(GitHubEvent gitHubEvent)
    {
        return $"Commented on pull request #{PullRequest.Number} in '{gitHubEvent.Repository.Name}'";
    }
}