using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Models;  
public record RemoveMemberModel :IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid AdminId { get; set; }
    public Guid MemberId { get; set;}
}
