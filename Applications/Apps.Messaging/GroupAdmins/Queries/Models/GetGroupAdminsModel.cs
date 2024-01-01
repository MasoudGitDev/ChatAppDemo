using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Queries.Models;  
public record GetGroupAdminsModel : IRequest<Result<List<AdminMemberResult>>> {
    public Guid GroupId { get; set; }

}
