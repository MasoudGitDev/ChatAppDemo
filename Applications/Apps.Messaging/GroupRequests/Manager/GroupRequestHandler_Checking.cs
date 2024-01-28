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
        return ( await GetRequestAsync(groupId , requesterId) ).CheckNullability(
            new ExceptionModel("GetRequestAsync" , "NotFound" , $"The request with groupId = {groupId} and requesterId = {requesterId} was not found"));
    }

    protected void CheckDescriptionValue(string? description , string? source) {
        if(string.IsNullOrWhiteSpace(description)) {
            throw new CustomException("GroupRequestHandler.CheckDescriptionValue" , "NullOrWhiteSpace" , "The <description> must be not null or white space.");
        }
        if(description.Trim().Length <= 2) {
            throw new CustomException("GroupRequestHandler.CheckDescriptionValue" , "LengthError" , "The <description> length must be > 2 characters.");
        }
        if(source == description) {
            throw new CustomException("GroupRequestHandler.CheckDescriptionValue" , "EqualityError" , "The <description> must be differ than <source>.");
        }
    }
}
