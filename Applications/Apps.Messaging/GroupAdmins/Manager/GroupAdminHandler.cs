using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Manager;
internal abstract partial class GroupAdminHandler<T, R>(IGroupAdminRepo groupAdminRepo)
    : IRequestHandler<T , R> where T : IRequest<R> where R : IResult {
    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
}


