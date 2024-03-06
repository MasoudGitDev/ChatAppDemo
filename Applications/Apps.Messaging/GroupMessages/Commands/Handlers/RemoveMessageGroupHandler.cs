using Apps.Messaging.GroupMessages.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Handlers;
internal sealed class RemoveMessageGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<RemoveMessageModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(RemoveMessageModel request , CancellationToken cancellationToken) {
        var message = (await GetCurrentMessageAsync(request.MessageId))
            .ThrowIfNull($"There is no any message with this id : <{request.MessageId}> .");

        if(message.AppUserId.Value != request.MessageId) {
            ( await GetAdminMemberAsync(message.GroupId , request.MemberId) )
                .ThrowIfNull("Just admins can remove other member messages.");
        }

        _unitOfWork.Remove(message);
        await _unitOfWork.SaveChangesAsync();
        return new Result(ResultStatus.Success ,
            new("RemoveGroupMessage" , "The message has been removed successfully."));

    }
}
