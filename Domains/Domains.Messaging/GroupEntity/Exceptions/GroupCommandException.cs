using Shared.Exceptions;
using Shared.Models;
namespace Domains.Messaging.GroupEntity.Exceptions;
public class GroupCommandException : CustomException {
    public GroupCommandException() {
    }

    public GroupCommandException(string message) : base(message) {
    }

    public GroupCommandException(ExceptionModel model) : base(model) {
    }

    public GroupCommandException(string code , string description) : base(code , description) {
    }
}
