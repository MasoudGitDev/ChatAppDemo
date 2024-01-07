using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.ValueObjects;
namespace Domains.Messaging.GroupMemberEntity.Repos;  
public interface IGroupAdminCommands {
    Task ConfirmedRequest(GroupMemberTbl member , GroupRequestTbl request);
    Task DeleteMemberAsync(GroupMemberTbl member);
    Task DeleteGroupAsync(GroupTbl group , List<GroupMemberTbl> members , List<GroupRequestTbl> requests);
    Task UnblockAsync(GroupMemberTbl member);
    Task BlockAsync(GroupMemberTbl member , EntityId adminId , DateTime startAt , DateTime? endAt , string? reason);
    Task ToAdminAsync(GroupMemberTbl member , EntityId adminId , DateTime startAt , DateTime? endAt , string? reason);
    Task ToNormalMemberAsync(GroupMemberTbl adminMember );
    Task ChangeRequestableStateAsync(GroupTbl group , bool isRequestable);


    //Task DeleteLogoAsync(GroupTbl group , string);
    Task AddLogoAsync(GroupTbl group,Logo logo);
    Task DeleteLogosAsync(GroupTbl group);
    Task ChangeDisplayIdAsync(GroupTbl group , DisplayId newDisplayId);
    Task ChangeInfoAsync(GroupTbl group , string title , string description);
}
