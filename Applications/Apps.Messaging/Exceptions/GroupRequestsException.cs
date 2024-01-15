using Shared.Exceptions;
namespace Apps.Messaging.Exceptions;

public class GroupRequestsException : CustomException {
    public GroupRequestsException(string where , string code , string description) : base(where , code , description) {
    }
}
