using Shared.Exceptions;
using Shared.Models;
namespace Apps.Messaging.Exceptions;

public class GroupRequestsException : CustomException {
    public GroupRequestsException() {
    }

    public GroupRequestsException(string message) : base(message) {
    }

    public GroupRequestsException(ExceptionModel model) : base(model) {
    }

    public GroupRequestsException(string code , string description) : base(code , description) {
    }
}
