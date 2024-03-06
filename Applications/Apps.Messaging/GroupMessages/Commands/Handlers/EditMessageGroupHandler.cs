using Apps.Messaging.GroupMessages.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Handlers;
internal sealed class EditMessageGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<EditMessageModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(EditMessageModel request , CancellationToken cancellationToken) {

        var message = (await GetCurrentMessageAsync(request.MessageId))
            .ThrowIfNull($"There is no any message with this id : <{request.MessageId}> .");

        if(message.AppUserId.Value != request.MemberId) {
            throw new NotPossibleException("You can not edit other member messages.");
        }

        message.Update(request.Message , request.FileUrl);

        await _unitOfWork.SaveChangesAsync();
        return new Result(ResultStatus.Success ,
            new("UpdateGroupMessage" , "The message has been updated successfully."));
    }
}
