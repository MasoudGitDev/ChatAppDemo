using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Apps.Messaging.Managers;

// GroupRequest CRUD
internal abstract partial class GroupRequestHandler<T,R>{
    protected async Task CreateAsync(Guid groupId , Guid requesterId , string? description)
        => await groupRequestRepo.Commands.CreateAsync(new GroupRequestTbl() {
            GroupId = new GroupId(groupId) ,
            RequesterId = new AppUserId(requesterId) ,
            Id = new GroupRequestId() ,
            RequestNumbers = 1 ,
            IsBlocked = false ,
            RequestedAt = DateTime.UtcNow ,
            Description = description ,
        });

    protected async Task UpdateAsync(string? description , GroupRequestTbl groupRequest) {
        CheckDescriptionValue(description , groupRequest.Description);
        groupRequest.RequestNumbers = groupRequest.RequestNumbers + 1;
        groupRequest.Description = description;
        await groupRequestRepo.Commands.UpdateAsync(groupRequest);
    }

    protected async Task DeleteAsync(GroupRequestTbl groupRequest)
        => await groupRequestRepo.Commands.DeleteAsync(groupRequest); 
}
