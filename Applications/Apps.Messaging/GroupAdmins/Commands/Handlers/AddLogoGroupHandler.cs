using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class AddLogoGroupHandler(IGroupAdminRepo groupAdminRepo) : IRequestHandler<AddLogoModel , Result> {
    public async Task<Result> Handle(AddLogoModel request , CancellationToken cancellationToken) {
        var findGroup = await groupAdminRepo.General.Queries.GetGroupAsync(request.GroupId);
        if(findGroup == null) {
            return new Result(ResultStatus.Failed , new("GetGroupAsync" , "NotFound" , "GroupId is invalid."));
        }
        await groupAdminRepo.Commands.AddLogoAsync(findGroup , request.Logo);
        return new Result(ResultStatus.Success , null);
    }
}
