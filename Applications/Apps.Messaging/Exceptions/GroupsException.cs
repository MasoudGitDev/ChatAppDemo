using Shared.Exceptions;
using Shared.Models;
namespace Apps.Messaging.Exceptions;

public class GroupsException : CustomException {
    public GroupsException() {
    }

    public GroupsException(string message) : base(message) {
    }

    public GroupsException(ExceptionModel model) : base(model) {
    }

    public GroupsException(string code , string description) : base(code , description) {
    }
}
