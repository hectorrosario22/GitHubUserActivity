using System.Net.Http.Json;
using System.Text.Json;
using GitHubUserActivity.Interfaces;
using GitHubUserActivity.Models;
using Microsoft.Extensions.Options;

namespace GitHubUserActivity.Services;

public class GitHubApiService(
    IHttpClientFactory httpClientFactory) : IGitHubApiService
{
    public async Task<List<GitHubEvent>> GetEvents(string username)
    {
        var client = httpClientFactory.CreateClient("GitHubApi");
        var response = await client.GetAsync($"users/{username}/events");
        response.EnsureSuccessStatusCode();

        var events = await response.Content.ReadFromJsonAsync<List<GitHubEvent>>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        return events ?? [];
    }
}