using Apps.Messaging.Shared.ResultModels;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Models {
    public record GetUserGroupsModel : IRequest<Result<List<GroupResultModel>>> {
        public Guid AppUserId { get; set; }
    }
}
