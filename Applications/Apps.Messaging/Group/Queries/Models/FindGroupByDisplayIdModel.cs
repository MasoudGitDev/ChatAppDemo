using Apps.Messaging.Shared.ResultModels;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Group.Queries.Models;

public record FindGroupByDisplayIdModel : IRequest<Result<GroupResultModel>>
{
    public string DisplayId { get; set; }
}
