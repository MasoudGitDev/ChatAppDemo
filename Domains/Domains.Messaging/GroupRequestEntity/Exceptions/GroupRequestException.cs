using Shared.Exceptions;

namespace Domains.Messaging.GroupRequestEntity.Exceptions;
public class GroupRequestException : CustomException {
    public GroupRequestException(string where , string code , string description) : base(where , code , description) {
    }
}