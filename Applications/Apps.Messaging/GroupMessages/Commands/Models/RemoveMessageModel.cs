using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Models;
public class RemoveMessageModel : IRequest<Result> {
    public Guid MessageId { get; set; }
    public Guid MemberId { get; set; }
}
