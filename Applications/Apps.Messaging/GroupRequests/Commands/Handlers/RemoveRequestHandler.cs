using Apps.Messaging.GroupAdmins.Manager;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;

/// <summary>
///  Just requesters can delete the request.
///  Admins can block or accept the requests.
/// </summary>
internal sealed class RemoveRequestHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<RemoveRequestModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(RemoveRequestModel request , CancellationToken cancellationToken) {
        var requestModel = (await GetRequestAsync(request.GroupId , request.RequesterId))
            .ThrowIfNull(
            $"The request with groupId : <{request.GroupId}> " +
            $"and requesterId :<{request.RequesterId}> was not found.");
        Result? result = null;
        if(requestModel.IsBlocked) {
            requestModel.Visibility.VisibleToRequester(Visibility.Hidden);
            requestModel.Visibility.VisibleToAdmins(Visibility.Hidden); 
            result = new Result(ResultStatus.Success ,
                new("Warning" ,
                "The <request> can not <remove> completely! " +
                "Because it is blocked by one of the group admins."));
        }
        else {
            _unitOfWork.Remove(requestModel);
            result = new Result(ResultStatus.Success , 
                new("Delete" , "The <request> has been <removed> successfully"));
        }
       
        await SaveChangesAsync();
        return result;
    }
}
