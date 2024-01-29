using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.GroupRequestEntity.Exceptions;
public class GroupRequestException : CustomException {
    public GroupRequestException() {
    }

    public GroupRequestException(string message) : base(message) {
    }

    public GroupRequestException(ExceptionModel model) : base(model) {
    }

    public GroupRequestException(string code , string description) : base(code , description) {
    }
}