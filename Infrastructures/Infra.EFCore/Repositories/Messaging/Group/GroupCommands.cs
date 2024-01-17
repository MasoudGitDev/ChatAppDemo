﻿using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Exceptions;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupCommands(AppDbContext appDbContext) : IGroupCommands
{
    [ConcurrencyTryCatch<GroupCommandException>]
    public async Task LeaveGroupAsync(GroupMemberTbl member) {
        appDbContext.GroupMembers.Remove(member);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupCommandException>]
    public async Task CreateGroupAsync(GroupTbl group) {
        await appDbContext.Groups.AddAsync(group);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupCommandException>]
    public async Task CerateMemberAsync(GroupMemberTbl member) {
        await appDbContext.GroupMembers.AddAsync(member);
        await appDbContext.SaveChangesAsync();
    }
}
