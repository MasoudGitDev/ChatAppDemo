using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMembers.Queries.Models;  
public class GetMembersModel : IRequest<Result<List<MemberInfo>>> {
    public Guid GroupId { get; set; }
}
