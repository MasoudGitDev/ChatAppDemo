using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Infra.EFCore.Contexts;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupCommands(AppDbContext appDbContext) : IGroupCommands
{
    public async Task CreateRequestAsync(GroupRequestTbl request) {
        await TryToDoAsync(nameof(CreateRequestAsync) , async () => {
            await appDbContext.GroupRequests.AddAsync(request);
        });
    }

    public async Task LeaveGroupAsync(GroupMemberTbl member)
    {
        await TryToDoAsync(nameof(LeaveGroupAsync), async () => {
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

    public async Task CreateGroupAsync(GroupTbl group) {
        await TryToDoAsync(nameof(CreateGroupAsync), async () => {
            await appDbContext.Groups.AddAsync(group);
        });       
    }



    public async Task CerateMemberAsync(GroupMemberTbl member) {
        await TryToDoAsync(nameof(RemoveRequestAsync) , async () => {
            await appDbContext.GroupMembers.AddAsync(member);
        });
    }


    private async Task TryToDoAsync(string methodName , Func<Task> actions) {
        try {
            await actions.Invoke();
            await appDbContext.SaveChangesAsync();
        }
        catch(Exception ex) {
            Console.WriteLine("Error At :" + methodName +  ex.Message.ToString());
        }
    }
}
