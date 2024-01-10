using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Models;
public record GetGroupRequestModel : IRequest<Result<GroupRequesterResult>> {
    public Guid GroupId { get; set; }
    public Guid RequesterId { get; set; }
}
