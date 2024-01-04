using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;

namespace Domains.Messaging.GroupEntity.Repo;

public interface IGroupCommands {
    Task LeaveGroupAsync(GroupMemberTbl groupMemberEntity);

    Task SendRequestAsync(GroupRequestTbl requestEntity);
    Task UpdateRequestAsync(GroupRequestTbl requestEntity);
    Task RemoveRequestAsync(GroupRequestTbl requestEntity);
}