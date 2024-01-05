using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Models;  
public record BlockModel : IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
    public Guid AdminId { get; set; }
    public DateTime StartBlockAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndBlockAt { get; set; }
    public string? Reason { get; set; }
}
