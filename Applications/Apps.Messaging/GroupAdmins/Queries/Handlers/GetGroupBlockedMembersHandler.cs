using Apps.Messaging.Exceptions;
using Apps.Messaging.GroupAdmins.Queries.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Queries.Handlers;  
internal sealed class GetGroupBlockedMembersHandler(IGroupMemberQueries _queries)
    : IRequestHandler<GetBlockedMembersModel , Result<List<BlockMemberResult>>> {
    public async Task<Result<List<BlockMemberResult>>> Handle(GetBlockedMembersModel request , CancellationToken cancellationToken) {
        var adminMember = await _queries.GetAdminMemberInfoAsync(request.GroupId,request.AdminId); 
        if (adminMember == null) {
           throw new GroupAdminManagerException( "NotAccess" , "You are not an admin.");
        }
        var blockedMembers = await _queries.GetBlockedMembersAsync(request.GroupId);
        return new Result<List<BlockMemberResult>>(ResultStatus.Success , null , blockedMembers);
    }
}
