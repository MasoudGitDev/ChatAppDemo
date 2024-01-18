using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Models;

namespace Apps.Messaging.Managers;
internal abstract class GroupRequestHandler<T,R> (IGroupRequestRepo groupRequestRepo)
    : IRequestHandler<T , R> where T : IRequest<R> where R : IResult {

    public virtual Task<R> Handle(T request , CancellationToken cancellationToken) {
        throw new NotImplementedException("Overwrite this method.");
    }

    // Checking
    protected async Task<GroupRequestTbl> GetRequestWithCheckingAsync(GroupId groupId, AppUserId requesterId)
    {
        var groupRequest = await groupRequestRepo.Queries.GetRequestAsync(groupId, requesterId);
        if (groupRequest == null)
        {
            throw new CustomException("GetRequestAsync", "NotFound", "GroupId or RequesterId or both are invalid.");
        }
        return groupRequest;
    }
    protected void CheckDescriptionValue(string? description, string? source)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Trim().Length <= 2 || source == description)
        {
            throw new CustomException("CheckDescriptionValue", "SameOrEmpty", "The description is empty or same!");
        }
    }

    // Main U C R operations
    protected async Task UpdateAsync(string? description, GroupRequestTbl groupRequest)
    {
        CheckDescriptionValue(description, groupRequest.Description);
        groupRequest.RequestNumbers = groupRequest.RequestNumbers + 1;
        groupRequest.Description = description;
        await groupRequestRepo.Commands.UpdateAsync(groupRequest);
    }
    protected async Task CreateAsync(Guid groupId, Guid requesterId, string? description)
    {
        await groupRequestRepo.Commands.CreateAsync(new GroupRequestTbl()
        {
            GroupId = new GroupId(groupId),
            RequesterId = new AppUserId(requesterId),
            Id = new GroupRequestId(Guid.NewGuid()),
            RequestNumbers = 1,
            IsBlocked = false,
            RequestedAt = DateTime.UtcNow,
            Description = description,
        });
    }
    protected async Task RemoveAsync(GroupRequestTbl groupRequest) => await groupRequestRepo.Commands.DeleteAsync(groupRequest);

    // Result base
    protected async Task<Result> CreateOrUpdateAsync(GroupId groupId, AppUserId requesterId, string? description)
    {
        var groupRequest = await groupRequestRepo.Queries.GetRequestAsync(groupId, requesterId);
        if (groupRequest != null)
        {
            await UpdateAsync(description, groupRequest);
        }
        else
        {
            await CreateAsync(groupId, requesterId, description);
        }
        return new Result(ResultStatus.Success, null);
    }
    protected async Task<Result> TryToAsync(GroupId groupId,AppUserId groupRequestId, Func<GroupRequestTbl, Task> actions)
    {
        var groupRequest = await GetRequestWithCheckingAsync(groupId, groupRequestId);
        await actions.Invoke(groupRequest);
        return new Result(ResultStatus.Success, null);
    }
}
