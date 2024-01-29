using Apps.Messaging.Groups.Queries.Models;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.GroupEntity.Exceptions;
using Domains.Messaging.GroupEntity.Repo;
using Mapster;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Handlers;
internal sealed class GetGroupsBySearchTextHandler (IGroupRepo groupRepo)
    : IRequestHandler<GetGroupsBySearchTextModel , Result<List<GroupResultModel>>> {
    public async Task<Result<List<GroupResultModel>>> Handle(GetGroupsBySearchTextModel request , CancellationToken cancellationToken) {
        if(String.IsNullOrWhiteSpace(request.SearchText.Trim())) {
            throw new GroupException("NullOrWhiteSpace" , "<searchText> can not be null or white space!");
        }
        var groups = (await groupRepo.Queries.GetGroupsBySearchTextAsync(request.SearchText));
        var groupDTOs = groups.Adapt<List<GroupResultModel>>();
        return new Result<List<GroupResultModel>>(ResultStatus.Success ,null , groupDTOs);
    }
}
