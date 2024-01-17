using Domains.Messaging.GroupMemberEntity;
namespace Domains.Messaging.GroupEntity.Repo;

public interface IGroupCommands {
    Task CreateGroupAsync(GroupTbl group);
    Task LeaveGroupAsync(GroupMemberTbl member);
    Task CerateMemberAsync(GroupMemberTbl member);
}