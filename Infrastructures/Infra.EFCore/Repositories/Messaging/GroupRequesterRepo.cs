﻿using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging;
internal class GroupRequesterRepo(AppDbContext appDbContext) : IGroupRequesterRepo {
    public async Task CreateAsync(GroupRequestTbl entity) {
        await TryToAsync(nameof(CreateAsync) , () => appDbContext.GroupRequesters.Add(entity));
    }

    public async Task<GroupRequestTbl?> GetAsync(EntityId groupId , EntityId requesterId) {
        return await appDbContext.GroupRequesters.FirstOrDefaultAsync(x => x.GroupId == groupId && x.RequesterId == requesterId);
    }

    public async Task RemoveAsync(GroupRequestTbl entity) {
        await TryToAsync(nameof(RemoveAsync) , () => appDbContext.GroupRequesters.Remove(entity));
    }

    public async Task UpdateAsync(GroupRequestTbl entity) {
        await TryToAsync(nameof(UpdateAsync) , () => appDbContext.GroupRequesters.Update(entity));
    }
    private async Task TryToAsync(string methodName,Action action) {
        try {
           action.Invoke();
           await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex) {
            throw new CustomException(methodName , "FailedOperation" , ex.Message);
        }
    }
}
