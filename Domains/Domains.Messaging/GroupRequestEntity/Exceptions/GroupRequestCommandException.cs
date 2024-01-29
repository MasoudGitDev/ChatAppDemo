using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.GroupRequestEntity.Exceptions;

public class GroupRequestCommandException : CustomException {
    public GroupRequestCommandException() {
    }

    public GroupRequestCommandException(string message) : base(message) {
    }

    public GroupRequestCommandException(ExceptionModel model) : base(model) {
    }

    public GroupRequestCommandException(string code , string description) : base(code , description) {
    }
}
