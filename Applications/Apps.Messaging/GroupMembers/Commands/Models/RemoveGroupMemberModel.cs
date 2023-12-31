using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMembers.Commands.Models;  
public record RemoveGroupMemberModel :IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
}
