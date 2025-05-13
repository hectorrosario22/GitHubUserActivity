using GitHubUserActivity.Models;

namespace GitHubUserActivity.Interfaces;

public interface IGitHubApiService
{
    Task<List<GitHubEvent>> GetEvents(string username);
}