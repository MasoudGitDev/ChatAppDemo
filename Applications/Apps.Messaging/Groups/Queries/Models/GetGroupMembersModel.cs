using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Models;
public class GetGroupMembersModel : IRequest<Result<List<MemberInfo>>>
{
    public string DisplayId { get; set; }
}
