using GitHubUserActivity.Models;

namespace GitHubUserActivity.Interfaces;

public interface IGitHubApiService
{
    Task<Result<List<GitHubEvent>>> GetEvents(string username);
}
