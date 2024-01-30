using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Managers;

// GroupRequest Checking
internal abstract partial class GroupRequestHandler<T, R> {
    protected async Task<GroupRequestTbl> GetRequestWithCheckingAsync(GroupId groupId , AppUserId requesterId) {
        return ( await GetRequestAsync(groupId , requesterId) )
            .IfIsNull(new ExceptionModel(
                "GroupRequestHandler" ,
                "GetRequestWithCheckingAsync" ,
                "NotFound" ,
                $"The request with groupId = {groupId} and requesterId = {requesterId} was not found"));
    }
    // CompareRequestDescriptionWithSource
    protected void CheckDescriptionValue(string? description , string? groupRequestDescription) {
        if(string.IsNullOrWhiteSpace(description)) {
            throw new CustomException(new ExceptionModel("GroupRequestHandler","CheckDescriptionValue" , "NullOrWhiteSpace" , "The <description> must be not null or white space."));
        }
        if(description.Trim().Length <= 2) {
            throw new CustomException(new ExceptionModel("GroupRequestHandler" , "CheckDescriptionValue" , "LengthError" , "The <description> length must be > 2 characters."));
        }
        if(groupRequestDescription == description) {
            throw new CustomException(new ExceptionModel("GroupRequestHandler" , "CheckDescriptionValue" , "EqualityError" , "The <description> must be differ than <source>."));
        }
    }
}
