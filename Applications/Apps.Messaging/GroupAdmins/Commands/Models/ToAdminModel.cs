using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Models;
public record ToAdminMemberModel:IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid AdminId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public string? Reason { get; set; }
    public AdminLevel LevelToAssign { get; set; } = AdminLevel.Regular;
}
