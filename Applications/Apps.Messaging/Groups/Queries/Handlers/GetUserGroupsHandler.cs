using Apps.Messaging.Groups.Queries.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.Shared.ValueObjects;
using Mapster;
using MediatR;
using Shared.DTOs.Group;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Handlers;
internal sealed class GetUserGroupsHandler(IGroupRepo groupRepo)
    : IRequestHandler<GetUserGroupsModel , Result<LinkedList<GroupResultDto>>> {
    public async Task<Result<LinkedList<GroupResultDto>>> Handle(GetUserGroupsModel request , CancellationToken cancellationToken) {
        var groups = (await groupRepo.Queries.GetUserGroupsAsync(request.AppUserId));
        TypeAdapterConfig<DisplayId , string>.NewConfig().MapWith(x => x.Value);
        TypeAdapterConfig<LinkedList<Logo> , string>.NewConfig().MapWith(x => x.ToJson() ?? string.Empty);
        var groupDTOs =  groups.Adapt<LinkedList<GroupResultDto>>();
        return new Result<LinkedList<GroupResultDto>>(ResultStatus.Success , null , groupDTOs);
    }
}
