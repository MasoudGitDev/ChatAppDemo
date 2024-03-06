using Apps.Messaging.Group.Queries.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.DTOs.Group;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Group.Queries.Handlers;
internal sealed class GetUserGroupsHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<GetUserGroupsModel , Result<LinkedList<GroupResultDto>>>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result<LinkedList<GroupResultDto>>> Handle(GetUserGroupsModel request , CancellationToken cancellationToken) {
        return new Result<LinkedList<GroupResultDto>>(
            ResultStatus.Success ,
            null ,
            ToGroupDTOs(await GetUserGroupsAsync(request.AppUserId)));
    }

    private LinkedList<GroupResultDto> ToGroupDTOs(List<GroupTbl> groups) {
        TypeAdapterConfig<DisplayId , string>.NewConfig().MapWith(x => x.Value);
        TypeAdapterConfig<LinkedList<Logo> , string>.NewConfig().MapWith(x => x.ToJson() ?? string.Empty);
        return groups.Adapt<LinkedList<GroupResultDto>>();
    }
}
