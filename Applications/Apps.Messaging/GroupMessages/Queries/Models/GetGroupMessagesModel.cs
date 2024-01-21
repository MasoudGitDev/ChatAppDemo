using Apps.Messaging.GroupMessages.Shared;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Queries.Models;
public class GetGroupMessagesModel :IRequest<Result<List<MessageResult>>> {
    public Guid GroupId { get; set; }
}
