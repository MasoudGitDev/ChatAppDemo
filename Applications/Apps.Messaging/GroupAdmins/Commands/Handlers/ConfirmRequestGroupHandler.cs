using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ConfirmRequestGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<ConfirmGroupRequestModel , Result>(groupAdminRepo) {
    public async Task<Result> Handle(ConfirmGroupRequestModel request , CancellationToken cancellationToken) {
        await GetAdminWithCheckingAsync(request.GroupId,request.AdminId);
        var groupRequest = await GetRequestWithCheckingAsync(request.GroupId,request.RequesterId);
        var newMember = new GroupMemberTbl(){
            Id = Guid.NewGuid(),
            AdminInfo = null ,
            BlockMemberInfo = null ,
            IsAdmin = false ,
            IsBlocked = false ,
            GroupId = request.GroupId,
            MemberAt = DateTime.UtcNow,
            MemberId = request.RequesterId,
        };        
        await groupAdminRepo.Commands.ConfirmedRequest(newMember , groupRequest);
        return new Result(ResultStatus.Success , null);
    }
}
