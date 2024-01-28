using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;

namespace Apps.Messaging.Managers;

// GroupRequest Queries
internal abstract partial class GroupRequestHandler<T, R> {
    protected async Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId requesterId)
        => await groupRequestRepo.Queries.GetRequestAsync(groupId , requesterId);

    protected async  Task<List<GroupRequestTbl>> GetUserRequestsAsync(AppUserId requesterId)
       => await groupRequestRepo.Queries.GetUserRequestsAsync(requesterId);

    protected async Task<List<GroupRequestTbl>> GetGroupRequestsAsync(GroupId groupId)
       => await groupRequestRepo.Queries.GetGroupRequestsAsync(groupId);
}
