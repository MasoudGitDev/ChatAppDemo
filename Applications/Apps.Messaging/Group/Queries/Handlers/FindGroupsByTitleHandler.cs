using Apps.Messaging.Group.Queries.Models;
using Apps.Messaging.Shared.Manager;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Group.Queries.Handlers;
internal sealed class FindGroupsByTitleHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<FindGroupsByTitleModel , Result<List<GroupResultModel>>>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result<List<GroupResultModel>>> Handle(FindGroupsByTitleModel request , CancellationToken cancellationToken) {
        return new Result<List<GroupResultModel>>(
            ResultStatus.Success ,
            null ,
            ToGroupResultModels(await FindGroupsByTitleAsync(request.Title)));
    }

    private List<GroupResultModel> ToGroupResultModels(List<GroupTbl> groups) {
        return groups.Adapt<List<GroupResultModel>>();
    }
}
