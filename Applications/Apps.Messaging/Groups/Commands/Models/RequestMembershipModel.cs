using MediatR;
using Shared.Models;
namespace Apps.Messaging.Groups.Commands.Models;  
public record RequestMembershipModel:IRequest<Result> {
    public string DisplayId { get; set; }
    public Guid UserId { get; set; }
}
