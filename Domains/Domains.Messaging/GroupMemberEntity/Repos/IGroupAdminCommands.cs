using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
namespace Domains.Messaging.GroupMemberEntity.Repos;  
public interface IGroupAdminCommands {
    Task ConfirmedRequest(GroupMemberTbl member , GroupRequestTbl request);
    Task DeleteMemberAsync(GroupMemberTbl member);
    Task CreateMemberAsync(GroupMemberTbl member);
    Task DeleteGroupAsync(GroupTbl group , List<GroupMemberTbl> members , List<GroupRequestTbl> requests);
    Task UnblockMemberAsync(GroupMemberTbl member);
    Task BlockMemberAsync(GroupMemberTbl member , AppUserId adminId , DateTime? startAt , DateTime? endAt , string? reason);
    Task ToAdminMemberAsync(GroupMemberTbl member , AppUserId byWhomAdminId , AdminAccessLevels levelToAssign , DateTime? startAt , DateTime? endAt , string? reason);
    Task ToNormalMemberAsync(GroupMemberTbl adminMember );
    Task ChangeRequestableStateAsync(GroupTbl group , bool isRequestable);
    


    //Task DeleteLogoAsync(GroupTbl group , string);
    Task AddLogoAsync(GroupTbl group,Logo logo);
    Task DeleteLogosAsync(GroupTbl group);
    Task ChangeDisplayIdAsync(GroupTbl group , DisplayId newDisplayId);
    Task ChangeInfoAsync(GroupTbl group , string title , string description);
}
