using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.Groups.Commands.Handlers;  
internal class CreateAdminsHandler(IGroupUnitOfWork groupUnitOfWork) : IRequestHandler<CreateAdminsCModel , Result> {
    public Task<Result> Handle(CreateAdminsCModel request , CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
