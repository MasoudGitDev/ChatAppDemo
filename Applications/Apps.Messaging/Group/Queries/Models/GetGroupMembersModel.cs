using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Group.Queries.Models;
public class GetGroupMembersModel : IRequest<Result<List<MemberInfo>>>
{
    public GroupId GroupId { get; set; }
}
