# GitHub User Activity

CLI tool to fetch and display the latest public activity from a GitHub user using the GitHub API.

Project URL: [https://roadmap.sh/projects/github-user-activity](https://roadmap.sh/projects/github-user-activity)

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or higher
- Internet access (to query the public GitHub API)

## Features

- Retrieve the latest public events of a GitHub user.
- Simple command-line usage with username as argument.
- Handles errors like invalid usernames or API failures.
- No external libraries or frameworks required for HTTP requests.

## Getting Started

**Clone the repository**

```bash
git clone https://github.com/hectorrosario22/GitHubUserActivity.git
cd GitHubUserActivity
```

**Build the project**

```bash
dotnet build
```

### Usage

```bash
# Get all user activities
dotnet run -- <username>
```

**Example**

```bash
dotnet run -- hectorrosario22
```

**Sample Output**
```bash
- Pushed to repository: hectorrosario22/GitHubUserActivity
- Opened issue in: hectorrosario22/task-tracker-cli
- Starred repository: torvalds/linux
...
```

## Possible Improvements

- Filter events by type (e.g., PushEvent, IssueEvent).
- Display activity in a more structured format.
- Cache API responses for efficiency.
- Add support for pagination to fetch more than 30 events.