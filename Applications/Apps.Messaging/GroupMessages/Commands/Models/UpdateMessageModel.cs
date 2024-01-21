using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Models;
public class UpdateMessageModel : IRequest<Result> {
    public Guid MessageId { get; set; }
    public string? FileUrl { get; set; }
    public string Message { get; set; }
}
