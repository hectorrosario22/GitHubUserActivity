namespace GitHubUserActivity.Models;

public interface IGitHubPayload
{
    string ToString(GitHubEvent gitHubEvent);
}