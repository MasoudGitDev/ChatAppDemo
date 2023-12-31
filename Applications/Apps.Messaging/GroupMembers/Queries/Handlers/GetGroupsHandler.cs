using Apps.Messaging.GroupMembers.Queries.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupMembers.Queries.Handlers {
    internal sealed class GetGroupsHandler(IGroupMemberRepo groupMemberRepo)
        : IRequestHandler<GetGroupsModel , Result<List<GroupInfo>>> {
        public async Task<Result<List<GroupInfo>>> Handle(GetGroupsModel request , CancellationToken cancellationToken) {
            return new Result<List<GroupInfo>>(
                ResultStatus.Success , null ,await groupMemberRepo.GetGroupsAsync(request.UserId));
        }
    }
}
