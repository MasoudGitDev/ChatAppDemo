using Apps.Messaging.Groups.Queries.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Handlers
{
    internal sealed class GetGroupMembersHandler(IGroupRepo groupRepo)
        : IRequestHandler<GetGroupMembersModel, Result<List<MemberInfo>>>
    {
        public async Task<Result<List<MemberInfo>>> Handle(GetGroupMembersModel request, CancellationToken cancellationToken)
        {
            var members = await groupRepo.Queries.GetMembersAsync(request.GroupId);
            var membersInfo = members.Select(x =>new MemberInfo(x.Id, x.MemberAt, x.IsAdmin ,x.AdminInfo?.AccessLevel)).ToList();
            return new Result<List<MemberInfo>>(ResultStatus.Success, null, membersInfo);
        }
    }
}
