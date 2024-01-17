using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Exceptions;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;
using Shared.Abstractions.Messaging.Constants;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging.Group.Members;
internal class GroupAdminCommands(AppDbContext appDbContext) : IGroupAdminCommands
{
    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task BlockMemberAsync(GroupMemberTbl member, AppUserId adminId, DateTime? startAt, DateTime? endAt, string? reason)
    {
        member.IsBlocked = true;
        member.AdminInfo = null;
        member.IsAdmin = false;
        member.BlockMemberInfo = new BlockedMemberInfo() {
            Reason = reason ,
            AdminId = adminId ,
            StartAt = startAt ?? DateTime.UtcNow,
            EndAt = endAt
        };
        appDbContext.GroupMembers.Update(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task UnblockMemberAsync(GroupMemberTbl member)
    {
        member.IsBlocked = false;
        member.BlockMemberInfo = null;
        appDbContext.GroupMembers.Update(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task ConfirmedRequest(GroupMemberTbl member, GroupRequestTbl request)
    {
        appDbContext.GroupRequests.Remove(request);
        await appDbContext.GroupMembers.AddAsync(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task CreateGroupAsync(GroupTbl group)
    {
        await appDbContext.Groups.AddAsync(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task DeleteGroupAsync(GroupTbl group, List<GroupMemberTbl> members, List<GroupRequestTbl> requests)
    {
        appDbContext.GroupMembers.RemoveRange(members);
        appDbContext.GroupRequests.RemoveRange(requests);
        appDbContext.Groups.Remove(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task AddLogoAsync(GroupTbl group, Logo logo)
    {
        if(group.Logos == null) {
            group.Logos = new();
        }
        group.Logos.AddLast(logo);
        appDbContext.Groups.Update(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task DeleteLogosAsync(GroupTbl group)
    {
        group.Logos = null;
        appDbContext.Groups.Update(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task DeleteMemberAsync(GroupMemberTbl member)
    {
        appDbContext.GroupMembers.Remove(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task ChangeRequestableStateAsync(GroupTbl group, bool isRequestable)
    {
        group.IsRequestable = isRequestable;
        appDbContext.Groups.Update(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task ToAdminMemberAsync(GroupMemberTbl member, AppUserId byWhomAdminId , AdminAccessLevels levelToAssign , DateTime? startAt, DateTime? endAt, string? reason) {

        member.IsAdmin = true;
        member.AdminInfo = new() {
            Reason = reason ,
            StartAt = startAt ?? DateTime.UtcNow ,
            EndAt = endAt ,
            ByWhomId = byWhomAdminId,
            AccessLevel = levelToAssign,
        };
        appDbContext.GroupMembers.Update(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task ToNormalMemberAsync(GroupMemberTbl adminMember)
    {
        adminMember.IsAdmin = false;
        adminMember.AdminInfo = null;
        adminMember.IsBlocked = false;
        adminMember.BlockMemberInfo = null;
        appDbContext.GroupMembers.Update(adminMember);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task ChangeDisplayIdAsync(GroupTbl group, DisplayId newDisplayId)
    {
        group.DisplayId = newDisplayId;
        appDbContext.Groups.Update(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task ChangeInfoAsync(GroupTbl group, string title, string description)
    {
        group.Description = description;
        group.Title = title;
        appDbContext.Groups.Update(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task CreateMemberAsync(GroupMemberTbl member)
    {
        await appDbContext.GroupMembers.AddAsync(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupMemberException>]
    public async Task DeleteLogoAsync(EntityId groupId) {
        await Task.CompletedTask;
    }
}