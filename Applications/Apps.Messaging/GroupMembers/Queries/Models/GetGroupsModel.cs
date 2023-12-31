using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMembers.Queries.Models;  
public class GetGroupsModel : IRequest<Result<List<GroupInfo>>> {
    public Guid UserId { get; set; }
}
