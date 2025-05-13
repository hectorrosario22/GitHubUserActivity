using GitHubUserActivity.Models;

namespace GitHubUserActivity.Interfaces;

public interface IPrintService
{
    void PrintEvents(List<GitHubEvent> events);
}