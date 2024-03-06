using MediatR;
using Shared.Models;
namespace Apps.Messaging.GroupRequests.Commands.Models;
public record RequestMembershipModel : IRequest<Result>
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public string? Description { get; set; }
}
