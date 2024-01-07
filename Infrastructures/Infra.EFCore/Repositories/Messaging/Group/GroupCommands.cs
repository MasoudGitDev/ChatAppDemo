using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Infra.EFCore.Contexts;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupCommands(AppDbContext appDbContext) : IGroupCommands
{
    public Task SendRequestAsync(GroupRequestTbl request) {
        throw new NotImplementedException();
    }

    public async Task LeaveGroupAsync(GroupMemberTbl member)
    {
        await TryToDoAsync(async () => {
            appDbContext.GroupMembers.Remove(member);
            await Task.CompletedTask;
        });
    }

    public Task RemoveRequestAsync(GroupRequestTbl request) {
        throw new NotImplementedException();
    }

    public Task UpdateRequestAsync(GroupRequestTbl request) {
        throw new NotImplementedException();
    }

    public async Task CreateGroupAsync(GroupTbl group , GroupMemberTbl creator) {
        await TryToDoAsync(async () => {
            await appDbContext.Groups.AddAsync(group);
            await appDbContext.GroupMembers.AddAsync(creator);
        });       
    }

    private async Task TryToDoAsync(Func<Task> actions) {
        try {
            await actions.Invoke();
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }
}
