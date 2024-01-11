using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupRequestEntity.Repos;  
public interface IGroupRequestQueries {
    Task<List<GroupRequestTbl>> GetUserRequestsAsync(AppUserId appUserId);
    Task<List<GroupRequestTbl>> GetGroupRequestsAsync(GroupId groupId);
    Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId appUserId);
}
