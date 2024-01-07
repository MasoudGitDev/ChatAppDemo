using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;

namespace Domains.Messaging.GroupEntity.Repo;

public interface IGroupCommands {
    Task CreateGroupAsync(GroupTbl group,GroupMemberTbl creator);
    Task LeaveGroupAsync(GroupMemberTbl member);
    Task SendRequestAsync(GroupRequestTbl request);
    Task UpdateRequestAsync(GroupRequestTbl request);
    Task RemoveRequestAsync(GroupRequestTbl request);
}