using Shared.Exceptions;

namespace Domains.Messaging.GroupRequestEntity.Exceptions;

public class GroupRequestCommandException : CustomException {
    public GroupRequestCommandException(string where , string code , string description) : base(where , code , description) {
    }
}
