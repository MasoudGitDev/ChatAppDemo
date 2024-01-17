using Shared.Exceptions;

namespace Domains.Messaging.GroupMemberEntity.Exceptions;
public class GroupMemberException : CustomException {
    public GroupMemberException(string where , string code , string description) : base(where , code , description) {
    }
}
