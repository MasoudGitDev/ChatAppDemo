using Shared.Exceptions;

namespace Domains.Messaging.GroupMemberEntity.Exceptions;

public class GroupMemberCommandException : CustomException {
    public GroupMemberCommandException(string where , string code , string description) : base(where , code , description) {
    }
}
