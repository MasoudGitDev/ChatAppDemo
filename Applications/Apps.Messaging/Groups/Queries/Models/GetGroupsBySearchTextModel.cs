using Apps.Messaging.Shared.ResultModels;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Groups.Queries.Models;  
public record GetGroupsBySearchTextModel:IRequest<Result<List<GroupResultModel>>> {
    public string SearchText { get; set; }
}
