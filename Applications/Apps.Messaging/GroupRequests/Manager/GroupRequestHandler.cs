using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.Shared.ValueObjects;
using Mapster;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Managers;
internal abstract partial class GroupRequestHandler<T, R>(IGroupRequestRepo groupRequestRepo)
    : IRequestHandler<T , R> where T : IRequest<R> where R : IResult {

    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
    protected record TryToModel(GroupId GroupId , AppUserId RequesterId , EventType EventType);
    

    protected async Task<Result> CreateOrUpdateAsync(GroupId groupId , AppUserId requesterId , string? description) {        
        var groupRequest = await GetRequestAsync(groupId , requesterId);
        if(groupRequest != null) {
            await UpdateAsync(description , groupRequest);
            return new Result(ResultStatus.Success , new(nameof(CreateOrUpdateAsync) , "Update" , "The request has been updated successfully."));
        }
        await CreateAsync(groupId , requesterId , description);
        return new Result(ResultStatus.Success , new(nameof(CreateOrUpdateAsync) , "Create" , "The request has been created successfully."));
    }

    protected async Task<Result> TryToAsync(TryToModel model , Func<GroupRequestTbl , Task> actions) {
        var groupRequest = await GetRequestWithCheckingAsync(model.GroupId, model.RequesterId);
        await actions.Invoke(groupRequest);
        return new Result(ResultStatus.Success , new(typeof(GroupRequestHandler<,>).Name , model.EventType.ToString() , $"The request has been {model.EventType} successfully."));
    }

    protected async Task<Result<List<GroupRequestResult>>> GetGroupRequestsResultAsync(GroupId groupId) {
        var groupRequests = await GetGroupRequestsAsync(groupId);
        TypeAdapterConfig<AppUserId , Guid>.NewConfig().MapWith(x => x.Value);
        TypeAdapterConfig<GroupRequestTbl , GroupRequestResult>.NewConfig().Map(x => x.Id , x => x.RequesterId);
        return new Result<List<GroupRequestResult>>(ResultStatus.Success , null , groupRequests.Adapt<List<GroupRequestResult>>());
    }

    protected async Task<Result<List<GroupRequestResult>>> GetUserRequestsResultAsync(AppUserId requesterId) {
        var userRequests = await GetUserRequestsAsync(requesterId);
        TypeAdapterConfig<GroupId , Guid>.NewConfig().MapWith(x => x.Value);
        TypeAdapterConfig<GroupRequestTbl , GroupRequestResult>.NewConfig().Map(x => x.Id , x => x.GroupId);   
        return new Result<List<GroupRequestResult>>(ResultStatus.Success , null , userRequests.Adapt<List<GroupRequestResult>>());
    }
}
