using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Exceptions;

namespace Apps.Messaging.GroupRequests.Shared;  
internal abstract class GroupRequestManager(IGroupRequestRepo groupRequestRepo) {
    
    protected async Task<GroupRequestTbl> GetRequestWithCheckingAsync(GroupId groupId , AppUserId requesterId) {
        var groupRequest = await groupRequestRepo.Queries.GetRequestAsync(groupId , requesterId);
        if(groupRequest == null) {
            throw new CustomException("GetRequestAsync" , "NotFound" , "GroupId or RequesterId or both are invalid.");
        }
        return groupRequest;
    }
    protected void CheckDescriptionValue(string? description,string? source) {
        if( string.IsNullOrWhiteSpace(description) ||   description.Trim().Length <= 2 || source == description) {
            throw new CustomException("CheckDescriptionValue" , "SameOrEmpty" , "The description is empty or same!");
        }
    }
    protected async Task UpdateAsync(string? description , GroupRequestTbl groupRequest) {
        CheckDescriptionValue(description , groupRequest.Description);
        groupRequest.RequestNumbers = groupRequest.RequestNumbers + 1;
        groupRequest.Description = description;
        await groupRequestRepo.Commands.UpdateAsync(groupRequest);
    }
    protected async Task CreateAsync(Guid groupId , Guid requesterId,string? description) {
        await groupRequestRepo.Commands.CreateAsync(new GroupRequestTbl() {
            GroupId = new GroupId(groupId) ,
            RequesterId = new AppUserId(requesterId) ,
            Id = new GroupRequestId(Guid.NewGuid()) ,
            RequestNumbers = 1 ,
            IsBlocked = false ,
            RequestedAt = DateTime.UtcNow ,
            Description = description,
        });
    }
}
