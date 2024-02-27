namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupAdminRepo {
    IGroupAdminCommands Commands { get; }
    IGroupMemberQueries Queries { get; }
}
