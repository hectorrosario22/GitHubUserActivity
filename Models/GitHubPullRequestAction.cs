using System.Runtime.Serialization;

namespace GitHubUserActivity.Models;

public enum GitHubPullRequestAction
{
    Opened,
    Edited,
    Closed,
    Reopened,
    Assigned,
    Unassigned,
    [EnumMember(Value = "review_requested")]
    ReviewRequested,
    [EnumMember(Value = "review_request_removed")]
    ReviewRequestRemoved,
    Labeled,
    Unlabeled,
    Synchronize,
}
