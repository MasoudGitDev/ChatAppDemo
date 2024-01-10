using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;
namespace Apps.Messaging.Groups.Commands.Handlers;
internal sealed class LeaveGroupHandler(IGroupRepo groupRepo)
    : IRequestHandler<LeaveGroupModel, Result>
{
    public async Task<Result> Handle(LeaveGroupModel request, CancellationToken cancellationToken)
    {
        var findMember = await groupRepo.Queries.GetMemberAsync(request.GroupId, request.MemberId);
        if (findMember == null)
        {
            return new Result(ResultStatus.Failed, new("GetMemberAsync", "NotFound", "Invalid Ids or not found any row related to those ids."));
        }
        if(findMember.IsAdmin && findMember.AdminInfo != null && findMember.AdminInfo.AccessLevel == AdminAccessLevels.Creator) {
            return new Result(ResultStatus.Failed , new("LeaveGroupAsync" , "NotPossible" , "A group creator can not leave him/her groups.You can Delete the group."));
        }
        await groupRepo.Commands.LeaveGroupAsync(findMember);
        return new Result(ResultStatus.Success, null);
    }
}
