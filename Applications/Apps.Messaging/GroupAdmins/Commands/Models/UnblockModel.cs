using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Models;  
public record UnblockModel:IRequest<Result> {
    public Guid AdminId { get; set; }
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
}
