using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.UnitOfWorks;
using Shared.Extensions;
using Shared.Helpers;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class AddLogoGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<AddLogoModel , Result>(_unitOfWork) {
    public override async Task<Result> Handle(AddLogoModel request , CancellationToken cancellationToken) {

        ( await GetAdminMemberAsync(request.GroupId , request.AdminId) )
          .ThrowIfNull("You are not admin!");

        return await FileUploadHelper.DefaultUploadImageAsync(
            request.Logo ,
            $"Groups");
    }
}
