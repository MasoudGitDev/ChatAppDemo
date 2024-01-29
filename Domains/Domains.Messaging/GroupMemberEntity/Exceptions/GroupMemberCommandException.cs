using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.GroupMemberEntity.Exceptions;

public class GroupMemberCommandException : CustomException {
    public GroupMemberCommandException() {
    }

    public GroupMemberCommandException(string message) : base(message) {
    }

    public GroupMemberCommandException(ExceptionModel model) : base(model) {
    }

    public GroupMemberCommandException(string code , string description) : base(code , description) {
    }
}
