using Apps.Messaging.Group.Queries.Models;
using Apps.Messaging.Shared.Manager;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;


namespace Apps.Messaging.Group.Queries.Handlers;
internal sealed class FindGroupByDisplayIdHandler(IGroupMessagingUOW _unitOfWork) :
    GroupManager<FindGroupByDisplayIdModel , Result<GroupResultModel>>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result<GroupResultModel>> Handle(FindGroupByDisplayIdModel request , CancellationToken cancellationToken) {
        return new Result<GroupResultModel>(
            ResultStatus.Success ,
            null ,
            await ToGroupResultModel(request.DisplayId));
    }

    private async Task<GroupResultModel> ToGroupResultModel(DisplayId displayId) {
        var findGroup = (await GetGroupByDisplayIdAsync(displayId))
            .ThrowIfNull($"There is no any group with displayId : <{displayId}>");
        return findGroup.Adapt<GroupResultModel>();
    }
}
