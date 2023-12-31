using Apps.Messaging.GroupMembers.Queries.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupMembers.Queries.Handlers {
    internal sealed class GetMembersHandler(IGroupMemberRepo groupMemberRepo)
        : IRequestHandler<GetMembersModel , Result<List<MemberInfo>>> {
        public async Task<Result<List<MemberInfo>>> Handle(GetMembersModel request , CancellationToken cancellationToken) {
            return new Result<List<MemberInfo>>(
                ResultStatus.Success ,null , await groupMemberRepo.GetMembersAsync(request.GroupId));
        }
    }
}
