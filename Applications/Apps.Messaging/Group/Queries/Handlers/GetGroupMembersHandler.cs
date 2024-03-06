using Apps.Messaging.Group.Queries.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Group.Queries.Handlers;
internal sealed class GetGroupMembersHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<GetGroupMembersModel , Result<List<MemberInfo>>>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result<List<MemberInfo>>> Handle(GetGroupMembersModel request , CancellationToken cancellationToken) {
        return new Result<List<MemberInfo>>(
            ResultStatus.Success ,
            null ,
            ToMembersInfo(await GetMembersAsync(request.GroupId)));
    }

    private List<MemberInfo> ToMembersInfo(List<GroupMemberTbl> members) {
        TypeAdapterConfig<AdminMemberInfo? , AdminLevel?>.NewConfig().MapWith(x => x != null ? x.AdminLevel : null);
        return members.Adapt<List<MemberInfo>>();
    }
}
