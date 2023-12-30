using Domains.Messaging.GroupEntity;
using MediatR;
using Shared.Models;
using Domains.Messaging.GroupEntity.Repo;
using Apps.Messaging.Groups.Commands.Models;
namespace Apps.Messaging.Groups.Commands.Handlers;
internal class CreateHandler(IGroupUnitOfWork groupUnitOfWork) : IRequestHandler<CreateCModel, Result>
{
    
    public async Task<Result> Handle(CreateCModel request, CancellationToken cancellationToken)
        => await groupUnitOfWork.GroupRepo.CreateAsync(new GroupTbl
        {
            CreatorId = request.CreatorId,
            Members = new(),
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            GroupId = Guid.NewGuid(),
            DisplayId = request.DisplayId,
            Logos = new()
        });
}
