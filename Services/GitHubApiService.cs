using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Models;

namespace GitHubUserActivity.Services;

public class GitHubApiService(
    IHttpClientFactory httpClientFactory) : IGitHubApiService
{
    public async Task<Result<List<GitHubEvent>>> GetEvents(string username)
    {
        var client = httpClientFactory.CreateClient("GitHubApi");
        var response = await client.GetAsync($"users/{username}/events");

        if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotModified)
        {
            return Result<List<GitHubEvent>>.Failure($"Error {response.StatusCode} fetching events for user {username}: {response.ReasonPhrase}");
        }

        var events = await response.Content.ReadFromJsonAsync<List<GitHubEvent>>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        return Result<List<GitHubEvent>>.Success(events ?? []);
    }
}