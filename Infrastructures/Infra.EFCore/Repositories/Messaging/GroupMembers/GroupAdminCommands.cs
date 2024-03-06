//using Domains.Messaging.GroupEntity.Entity;
//using Domains.Messaging.GroupMemberEntity.Entity;
//using Domains.Messaging.GroupMemberEntity.Repos;
//using Domains.Messaging.GroupRequestEntity;
//using Domains.Messaging.Shared.ValueObjects;
//using Infra.EFCore.Contexts;
//using Shared.Abstractions.Messaging.Constants;

//namespace Infra.EFCore.Repositories.Messaging.GroupMembers;
//internal class GroupAdminCommands(AppDbContext appDbContext) : IGroupAdminCommands {

//    /// <summary>
//    /// Block=true and IsAmin=false
//    /// </summary>
//    public void BlockMember(
//        GroupMemberTbl member ,
//        AppUserId adminId ,
//        DateTime? startAt ,
//        DateTime? endAt ,
//        string? reason) {
//        member.IsBlocked = true;        
//        member.AdminInfo = null;
//        member.IsAdmin = false;
//        member.BlockMemberInfo = BlockedMemberInfo.Create(startAt, endAt , adminId , reason);
//        appDbContext.GroupMembers.Update(member);
//    }


//    public void UnblockMember(GroupMemberTbl member) {
//        member.IsBlocked = false;
//        member.BlockMemberInfo = null;
//        appDbContext.GroupMembers.Update(member);
//    }


//    public async Task ConfirmRequest(GroupMemberTbl member , GroupRequestTbl request) {
//        appDbContext.GroupRequests.Remove(request);
//        await appDbContext.GroupMembers.AddAsync(member);
//    }


//    public async Task CreateGroupAsync(GroupTbl group) {
//        await appDbContext.Groups.AddAsync(group);
//    }


//    public async Task DeleteGroupAsync(GroupTbl group , List<GroupMemberTbl> members , List<GroupRequestTbl> requests) {
//        appDbContext.GroupMembers.RemoveRange(members);
//        appDbContext.GroupRequests.RemoveRange(requests);
//        appDbContext.Groups.Remove(group);
//        await Task.CompletedTask;
//    }
 

//    public void DeleteMember(GroupMemberTbl member) {
//        appDbContext.GroupMembers.Remove(member);        
//    }  


//    public void ToAdminMember(GroupMemberTbl member ,
//        AppUserId byWhomAdminId ,
//        AdminType levelToAssign ,
//        DateTime? startAt ,
//        DateTime? endAt ,
//        string? reason) {

//        member.IsAdmin = true;
//        member.AdminInfo = new() {
//            Reason = reason ,
//            StartAt = startAt ?? DateTime.UtcNow ,
//            EndAt = endAt ,
//            ByWhomId = byWhomAdminId ,
//            AccessLevel = levelToAssign ,
//        };
//        appDbContext.GroupMembers.Update(member);
//    }


//    public void ToNormalMember(GroupMemberTbl adminMember) {
//        adminMember.IsAdmin = false;
//        adminMember.AdminInfo = null;
//        adminMember.IsBlocked = false;
//        adminMember.BlockMemberInfo = null;
//        appDbContext.GroupMembers.Update(adminMember);
//    }

//    public void Update(GroupTbl group) {
//        appDbContext.Groups.Update(group);
//    }


//    public async Task CreateMemberAsync(GroupMemberTbl member) {
//        await appDbContext.GroupMembers.AddAsync(member);
//    }

//    // For all members
//    public void LeaveGroup(GroupMemberTbl member)
//       => appDbContext.GroupMembers.Remove(member);
//}