using Shared.Exceptions;
using Shared.Models;

namespace Apps.Messaging.Exceptions;
internal class GroupMessageHandlerException : CustomException {
    public GroupMessageHandlerException() {
    }

    public GroupMessageHandlerException(string message) : base(message) {
    }

    public GroupMessageHandlerException(ExceptionModel model) : base(model) {
    }

    public GroupMessageHandlerException(string code , string description) : base(code , description) {
    }
}
