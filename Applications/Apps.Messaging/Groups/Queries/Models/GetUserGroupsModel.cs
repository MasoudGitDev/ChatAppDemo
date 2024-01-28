using MediatR;
using Shared.DTOs.Group;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Models {
    public record GetUserGroupsModel : IRequest<Result<LinkedList<GroupResultDto>>> {
        public Guid AppUserId { get; set; }
    }
}
