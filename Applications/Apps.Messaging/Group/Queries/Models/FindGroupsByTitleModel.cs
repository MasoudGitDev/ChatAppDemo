using Apps.Messaging.Shared.ResultModels;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Group.Queries.Models;
public record FindGroupsByTitleModel : IRequest<Result<List<GroupResultModel>>>
{
    public string Title { get; set; }
}
