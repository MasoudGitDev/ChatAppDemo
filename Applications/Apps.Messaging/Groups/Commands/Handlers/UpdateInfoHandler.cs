using MediatR;
using Shared.Models;
using Domains.Messaging.GroupEntity.Repo;
using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity.Models;
namespace Apps.Messaging.Groups.Commands.Handlers;
internal class UpdateInfoHandler(IGroupUnitOfWork groupUnitOfWork) : IRequestHandler<UpdateInfoModel, Result>
{
    public async Task<Result> Handle(UpdateInfoModel request , CancellationToken cancellationToken) {
        var model = await groupUnitOfWork.GroupRepo.GetAsync(request.GroupId);
        if(model.Success) {
            var updatedModel = new GroupInfoUpdateModel(){
                Description = model.Content.Description,
                DisplayId = model.Content.DisplayId,
            };
            return await groupUnitOfWork.UpdateRepo.UpdateInfoAsync(updatedModel , model.Content);
        }
        return new Result(false , model.Reports);
    }
}
