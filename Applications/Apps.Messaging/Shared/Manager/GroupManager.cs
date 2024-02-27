using Domains.Messaging.UnitOfWorks;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Manager;
/// <summary>
/// Manage Group operations
/// </summary>
internal abstract partial class GroupManager<T, R>(IGroupMessagingUOW _unitOfWork)
    : IRequestHandler<T , R> where T : IRequest<R> where R : IResult {
    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
    protected async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
}


