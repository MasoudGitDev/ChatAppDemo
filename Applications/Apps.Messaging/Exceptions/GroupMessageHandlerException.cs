using Shared.Exceptions;

namespace Apps.Messaging.Exceptions;
internal class GroupMessageHandlerException : CustomException {
    public GroupMessageHandlerException(string where , string code , string description) : base(where , code , description) {
    }
}
