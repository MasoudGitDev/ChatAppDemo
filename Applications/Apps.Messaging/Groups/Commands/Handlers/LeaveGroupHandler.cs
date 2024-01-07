using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;
using MediatR;
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
        await groupRepo.Commands.LeaveGroupAsync(findMember);
        return new Result(ResultStatus.Success, null);
    }
}
