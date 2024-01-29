using Shared.Exceptions;
using Shared.Models;
namespace Apps.Messaging.Exceptions;
public class GroupAdminsException : CustomException {
    public GroupAdminsException() {
    }

    public GroupAdminsException(string message) : base(message) {
    }

    public GroupAdminsException(ExceptionModel model) : base(model) {
    }

    public GroupAdminsException(string code , string description) : base(code , description) {
    }
}