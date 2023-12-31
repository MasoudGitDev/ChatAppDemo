using Apps.Messaging.GroupMembers.Commands.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Enums;
using Shared.Models;
namespace Apps.Messaging.GroupMembers.Commands.Handlers {
    internal sealed class RemoveGroupMemberHandler(IGroupMemberRepo groupMemberRepo)
        : IRequestHandler<RemoveGroupMemberModel , Result> {
        public async Task<Result> Handle(RemoveGroupMemberModel request , CancellationToken cancellationToken) {
            var findMember = await groupMemberRepo.GetMemberAsync(request.GroupId,request.MemberId);
            if(findMember == null) {
                return new Result(ResultStatus.Failed , new("GetMemberAsync" , "NotFound" , "Invalid Ids or not found any row related to those ids."));
            }
            await groupMemberRepo.RemoveMemberAsync(findMember);
            return new Result(ResultStatus.Success , null);
        }
    }
}
