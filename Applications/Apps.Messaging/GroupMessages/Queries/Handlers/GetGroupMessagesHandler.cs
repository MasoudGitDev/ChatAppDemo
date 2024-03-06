using Apps.Messaging.GroupMessages.Queries.Models;
using Apps.Messaging.GroupMessages.Shared;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupMessageEntity.Aggregate;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Queries.Handlers;
internal class GetGroupMessagesHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<GetGroupMessagesModel , Result<List<MessageResult>>>(_unitOfWork) {
    public override async Task<Result<List<MessageResult>>> Handle(GetGroupMessagesModel request , CancellationToken cancellationToken) {
        var messages = await GetGroupMessagesAsync(request.GroupId);
        return ToMessageResults(messages);
    }

    private Result<List<MessageResult>> ToMessageResults(List<GroupMessageTbl> messages) {
        var msgDTOs = messages.Adapt<List<MessageResult>>();
        return new Result<List<MessageResult>>(
            ResultStatus.Success ,
            new("GroupMessages" , $"<{messages.Count()}> messages has been founded.") ,
            msgDTOs);
    }
}
