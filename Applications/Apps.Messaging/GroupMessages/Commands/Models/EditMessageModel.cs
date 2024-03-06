using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Models;
public class EditMessageModel : IRequest<Result> {
    public Guid MessageId { get; set; }
    public string? FileUrl { get; set; }
    public string Message { get; set; }

    public Guid MemberId { get; set; }
}
