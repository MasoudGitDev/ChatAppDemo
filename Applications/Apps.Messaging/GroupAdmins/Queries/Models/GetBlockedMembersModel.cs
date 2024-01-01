using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Queries.Models;  
public record GetBlockedMembersModel : IRequest<Result<List<BlockMemberResult>>> {
    public Guid GroupId { get; set; }
    public Guid AdminId { get; set; }
}
