using MediatR;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Models;  
public record RemoveGroupModel :IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid OwnerId { get; set; }
}
