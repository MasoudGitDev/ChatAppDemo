using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Exceptions;
using Domains.Messaging.GroupRequestEntity.Repos;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;

namespace Infra.EFCore.Repositories.Messaging.GroupRequests;

[ConcurrencyTryCatch<GroupRequestCommandException>]
internal class GroupRequestCommands(AppDbContext appDbContext) : IGroupRequestCommands
{

    public async Task CreateAsync(GroupRequestTbl groupRequest)
        => await appDbContext.GroupRequests.AddAsync(groupRequest, cancellationToken: new CancellationToken());

    public void Delete(GroupRequestTbl groupRequest)
        => appDbContext.GroupRequests.Remove(groupRequest);

    public void Update(GroupRequestTbl groupRequest)
        => appDbContext.GroupRequests.Update(groupRequest);

}
