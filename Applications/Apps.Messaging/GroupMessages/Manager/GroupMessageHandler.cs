using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Manager;
internal abstract partial class GroupMessageHandler<T, R>(IGroupAdminRepo groupAdminRepo)
    : BaseGroupManager<T , R>(groupAdminRepo), IRequestHandler<T , R> where T : IRequest<R> where R : IResult {

}
