using Domains.Messaging.GroupEntity;
using MediatR;
using Shared.Models;
using Domains.Messaging.GroupEntity.Repo;
using Apps.Messaging.Groups.Commands.Models;
namespace Apps.Messaging.Groups.Commands.Handlers;
internal class CreateHandler(IGroupRepo groupRepo) : IRequestHandler<CreateModel, Result>
{

    public async Task<Result> Handle(CreateModel request, CancellationToken cancellationToken)
        => await groupRepo.CreateAsync(new GroupTbl
        {
            CreatorId = request.CreatorId,
            Admins = new(),
            Members = new(),
            CreatedAt = DateTime.UtcNow,
            Description = request.Description ?? string.Empty,
            GroupId = Guid.NewGuid(),
            DisplayId = request.DisplayId,
            Logos = new()
        });
}
