using Apps.Messaging.GroupAdmins.Queries.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Queries.Handlers;  
internal sealed class GetGroupBlockedMembersHandler(IGroupAdminRepo groupAdminRepo)
    : IRequestHandler<GetBlockedMembersModel , Result<List<BlockMemberResult>>> {
    public async Task<Result<List<BlockMemberResult>>> Handle(GetBlockedMembersModel request , CancellationToken cancellationToken) {
        var adminMember = await groupAdminRepo.Queries.IsAdminAsync(request.GroupId,request.AdminId); 
        if (adminMember == null) {
            new Result<List<BlockMemberResult>>(ResultStatus.Failed , new("GetAdminAsync" , "NotAccess" , "You are not an admin.") , null);
        }
        var blockedMembers = await groupAdminRepo.Queries.GetBlockedMembersAsync(request.GroupId);
        return new Result<List<BlockMemberResult>>(ResultStatus.Success , null , blockedMembers);
    }
}
