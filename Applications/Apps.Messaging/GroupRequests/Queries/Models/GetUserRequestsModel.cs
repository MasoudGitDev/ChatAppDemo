using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Models;

public record GetUserRequestsModel : IRequest<Result<List<GroupRequestResult>>> {
    public Guid RequesterId { get; set; }
    public Visibility Visibility { get; set; } = Visibility.Visible;
}
