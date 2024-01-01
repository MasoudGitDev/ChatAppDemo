using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Models;
public record ConvertToAdminModel:IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid AdminId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime StartAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndAt { get; set; }
    public string? Reason { get; set; }
}
