using Apps.Messaging.Groups.Queries.Models;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.GroupEntity.Repo;
using Mapster;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Handlers;
internal sealed class GetUserGroupsHandler(IGroupRepo groupRepo)
    : IRequestHandler<GetUserGroupsModel , Result<List<GroupResultModel>>> {
    public async Task<Result<List<GroupResultModel>>> Handle(GetUserGroupsModel request , CancellationToken cancellationToken) {
        var groups = (await groupRepo.Queries.GetUserGroupsAsync(request.AppUserId));
        var groupDTOs = groups.Adapt<List<GroupResultModel>>();
        return new Result<List<GroupResultModel>>(ResultStatus.Success , null , groupDTOs);
    }
}
