using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Models;  
public record GroupRequestableStateModel:IRequest<Result> {
    public Guid GroupId { get; set; }
    public bool IsRequestable { get; set; }
}
