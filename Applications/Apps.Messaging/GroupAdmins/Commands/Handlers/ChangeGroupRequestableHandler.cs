using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.UnitOfWorks;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ChangeGroupRequestableHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<GroupRequestableStateModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(GroupRequestableStateModel request , CancellationToken cancellationToken) {
        var findGroup = (await GetGroupAsync(request.GroupId))
            .ThrowIfNull($"The group with id : <{request.GroupId}> was not found.");

        var ownerInfo = (await GetAdminMemberInfoAsync(request.GroupId ,request.AdminId))
            .ThrowIfNull("You are not admin.");

        if(ownerInfo.AdminLevel is not AdminLevel.Owner) {
            throw new NotAccessException("Sorry , just owner can change <requestable> state of the group.");
        }

        findGroup.ChangeRequestableState(request.IsRequestable);
        await SaveChangesAsync();


        return new Result(ResultStatus.Success ,
            new("GroupRequestable" ,
            $"The Group with id:{findGroup.DisplayId} has changed to <Requestable> : <{request.IsRequestable}> "));
    }

}
