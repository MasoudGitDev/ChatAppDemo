using Domains.Messaging.GroupMemberEntity;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;
using Shared.ValueObjects;

namespace Apps.Messaging.Groups.Commands.Models;
public record CreateAdminsCModel : IRequest<Result> {
    public EntityId GroupId { get; set; }
    public LinkedList<GroupMemberTbl> Members { get; set; }
    public AdminAccessLevels AccessLevel { get; set; } = AdminAccessLevels.Low;
}
