using Apps.Messaging.GroupRequesters.Queries.Models;
using Domains.Messaging.GroupRequesterEntity.Repos;
using MediatR;
using Shared.Models;
using Shared.Enums;
using Shared.Extensions;

namespace Apps.Messaging.GroupRequesters.Queries.Handlers;
internal sealed class GetGroupRequesterHandler(IGroupRequesterRepo groupRequesterRepo)
    : IRequestHandler<GetGroupRequesterModel , Result<string>> {
    public async Task<Result<string>> Handle(GetGroupRequesterModel request , CancellationToken cancellationToken) {
       return new Result<string>(ResultStatus.Success , null ,
           (await groupRequesterRepo.GetAsync(request.GroupId , request.RequesterId))?.ToJson() ?? String.Empty);
    }
}
