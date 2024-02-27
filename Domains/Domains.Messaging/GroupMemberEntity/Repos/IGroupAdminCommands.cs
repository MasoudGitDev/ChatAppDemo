using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupRequestEntity;
namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupAdminCommands {

    //Task CreateGroupAsync(GroupTbl group);
    //void LeaveGroup(GroupMemberTbl member);


    Task ConfirmRequest(GroupMemberTbl member , GroupRequestTbl request);
    //   void DeleteMember(GroupMemberTbl member);
    //  Task CreateMemberAsync(GroupMemberTbl member);
    //   Task DeleteGroupAsync(GroupTbl group , List<GroupMemberTbl> members , List<GroupRequestTbl> requests);

    //  void UnblockMember(GroupMemberTbl member);
    //  void BlockMember(GroupMemberTbl member , AppUserId adminId , DateTime? startAt , DateTime? endAt , string? reason);
    // void ToAdminMember(GroupMemberTbl member , AppUserId byWhomAdminId , AdminType levelToAssign , DateTime? startAt , DateTime? endAt , string? reason);

    //void ToNormalMember(GroupMemberTbl adminMember );
    // void ChangeRequestableState(GroupTbl group , bool isRequestable);  
    //Task DeleteLogoAsync(GroupTbl group , string);
    //void AddLogo(GroupTbl group,Logo logo);
    //void DeleteLogos(GroupTbl group);
    //void DeleteLogo();
    // void ChangeDisplayId(GroupTbl group , DisplayId newDisplayId);
    // void ChangeInfo(GroupTbl group , string title , string description);

    // void Update(GroupTbl group);
}
