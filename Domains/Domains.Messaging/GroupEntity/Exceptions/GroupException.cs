using Shared.Exceptions;
using Shared.Models;
namespace Domains.Messaging.GroupEntity.Exceptions;
public class GroupException : CustomException {
    public GroupException() {
    }

    public GroupException(string message) : base(message) {
    }

    public GroupException(ExceptionModel model) : base(model) {
    }

    public GroupException(string code , string description) : base(code , description) {
    }
}
