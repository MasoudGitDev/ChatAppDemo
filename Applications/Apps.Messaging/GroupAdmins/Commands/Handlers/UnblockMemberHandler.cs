using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class UnblockMemberHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<UnblockMemberModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(UnblockMemberModel request , CancellationToken cancellationToken) {

        var targetMember = (await GetMemberAsync(request.GroupId, request.MemberId))
            .ThrowIfNull($"Not found any members with id :{request.MemberId}");
        // very important
        if(targetMember.IsBlocked is false) {
            throw new NotPossibleException("System can just unblock those members that was blocked before!");
        }

        //not need but for ensure
        if(targetMember.IsAdmin) {
            throw new NotPossibleException("An unblock member can not be an admin");
        }

        ( await GetAdminMemberAsync(request.GroupId , request.AdminId) )
            .ThrowIfNull("You are not admin!");

        targetMember.UnBlock();
        await SaveChangesAsync();

        return new Result(ResultStatus.Success , new("Unblock" , "The member has been unblocked successfully."));

    }
}