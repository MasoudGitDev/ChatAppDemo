using Apps.Messaging.GroupAdmins.Queries.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Queries.Handlers;
internal sealed class GetGroupAdminsHandler(IGroupAdminRepo groupAdminRepo)
    : IRequestHandler<GetGroupAdminsModel , Result<List<AdminMemberResult>>>{
    public async Task<Result<List<AdminMemberResult>>> Handle(GetGroupAdminsModel request , CancellationToken cancellationToken) {
        return new Result<List<AdminMemberResult>>(ResultStatus.Failed, null , await groupAdminRepo.Queries.GetAdminsAsync(request.GroupId));
    }
}
