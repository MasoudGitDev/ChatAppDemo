using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.GroupMemberEntity.Exceptions;
public class GroupMemberException : CustomException {
    public GroupMemberException() {
    }

    public GroupMemberException(string message) : base(message) {
    }

    public GroupMemberException(ExceptionModel model) : base(model) {
    }

    public GroupMemberException(string code , string description) : base(code , description) {
    }
}
