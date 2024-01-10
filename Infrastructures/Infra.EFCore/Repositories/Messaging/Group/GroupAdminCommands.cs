using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupAdminCommands(AppDbContext appDbContext) : IGroupAdminCommands {
    public async Task BlockAsync(GroupMemberTbl member , AppUserId adminId , DateTime startAt , DateTime? endAt , string? reason) {
        await TryToDoAsync(async () => {
            member.IsBlocked = true;
            member.BlockMemberInfo = new BlockedMemberInfo() {
                Reason = reason ,
                AdminId = adminId ,
                StartAt = startAt ,
                EndAt = endAt
            };
            appDbContext.GroupMembers.Update(member);
            await Task.CompletedTask;
        });
    }
    public async Task UnblockAsync(GroupMemberTbl member) {
        await TryToDoAsync(async () => {
            member.IsBlocked = false;
            member.BlockMemberInfo = null;
            appDbContext.GroupMembers.Update(member);
            await Task.CompletedTask;
        });
    }
    public async Task ConfirmedRequest(GroupMemberTbl member , GroupRequestTbl request) {
        await TryToDoAsync(async () => {
            appDbContext.GroupRequests.Remove(request);
            await appDbContext.GroupMembers.AddAsync(member);
        });
    }
    public async Task CreateGroupAsync(GroupTbl group) {
        await TryToDoAsync(async () => {
            await appDbContext.Groups.AddAsync(group);
        });
    }

    public async Task DeleteGroupAsync(GroupTbl group , List<GroupMemberTbl> members , List<GroupRequestTbl> requests) {
        await TryToDoAsync(async () => {
            appDbContext.GroupMembers.RemoveRange(members);
            appDbContext.GroupRequests.RemoveRange(requests);
            appDbContext.Groups.Remove(group);
            await Task.CompletedTask;
        });
    }

    public async Task DeleteLogoAsync(EntityId groupId) {
        await TryToDoAsync(async () => {
            // ??
            await Task.CompletedTask;
        });
    }
    public async Task AddLogoAsync(GroupTbl group, Logo logo) {
        await TryToDoAsync(async () => {
            if(group.Logos == null) {
                group.Logos = new();
            }
            group.Logos.AddLast(logo);
            appDbContext.Groups.Update(group);
            await Task.CompletedTask;
        });
    }

    public async Task DeleteLogosAsync(GroupTbl group) {
        await TryToDoAsync(async () => {
            group.Logos = null;
            appDbContext.Groups.Update(group);
            await Task.CompletedTask;
        });
    }
    public async Task DeleteMemberAsync(GroupMemberTbl member) {
        await TryToDoAsync(async () => {
            appDbContext.GroupMembers.Remove(member);
            await Task.CompletedTask;
        });
    }
    public async Task ChangeRequestableStateAsync(GroupTbl group , bool isRequestable) {
        await TryToDoAsync(async () => {
            group.IsRequestable = isRequestable;
            appDbContext.Groups.Update(group);
            await Task.CompletedTask;
        });
    }
    public async Task ToAdminAsync(GroupMemberTbl member , AppUserId adminId , DateTime startAt , DateTime? endAt , string? reason) {
        await TryToDoAsync(async () => {
            member.IsAdmin = true;
            member.AdminInfo = new() {
                Reason = reason ,
                StartAt = startAt ,
                EndAt = endAt ,
                ByWhomId = adminId
            };
            appDbContext.GroupMembers.Update(member);
            await Task.CompletedTask;
        });
    }
    public async Task ToNormalMemberAsync(GroupMemberTbl adminMember) {
        await TryToDoAsync(async () => {
            adminMember.IsAdmin = false;
            adminMember.AdminInfo = null;
            appDbContext.GroupMembers.Update(adminMember);
            await Task.CompletedTask;
        });
    }
    public async Task ChangeDisplayIdAsync(GroupTbl group , DisplayId newDisplayId) {
        await TryToDoAsync(async () => {
            group.DisplayId = newDisplayId;
            appDbContext.Groups.Update(group);
            await Task.CompletedTask;
        });
    }
    public async Task ChangeInfoAsync(GroupTbl group , string title , string description) {
        await TryToDoAsync(async () => {
            group.Description = description;
            group.Title = title;
            appDbContext.Groups.Update(group);
            await Task.CompletedTask;
        });
    }

    private async Task TryToDoAsync(Func<Task> actions) {
        try {
            await actions.Invoke();
            await appDbContext.SaveChangesAsync();
        }
        catch(Exception ex) {
            throw new Exception(ex.ToString());
        }
    }

    public async Task CreateMemberAsync(GroupMemberTbl member) {
        await TryToDoAsync(async () => {
            await appDbContext.GroupMembers.AddAsync(member);
        });
    }
}
