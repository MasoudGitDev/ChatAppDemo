using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Enums;

namespace Domains.Messaging.GroupRequestEntity.Repos;  
public interface IGroupRequestQueries {
    Task<List<GroupRequestTbl>> GetUserRequestsAsync(AppUserId appUserId , Visibility visible = Visibility.Visible);
    Task<List<GroupRequestTbl>> GetGroupRequestsAsync(GroupId groupId , Visibility visible = Visibility.Visible);
    Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId appUserId);
}
