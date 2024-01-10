using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;

namespace Domains.Messaging.GroupEntity.Repo;

public interface IGroupCommands {
    Task CreateGroupAsync(GroupTbl group);
    Task LeaveGroupAsync(GroupMemberTbl member);
    Task CerateMemberAsync(GroupMemberTbl member);
    Task CreateRequestAsync(GroupRequestTbl request);
    Task UpdateRequestAsync(GroupRequestTbl request);
    Task RemoveRequestAsync(GroupRequestTbl request);
}