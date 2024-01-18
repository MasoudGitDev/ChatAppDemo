using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Models;  
public record CreateGroupMessageModel : IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
    public string Message { get; set; }
    public string? FileUrl { get; set; }
}
