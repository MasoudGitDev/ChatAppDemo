using Shared.Exceptions;
namespace Apps.Messaging.Exceptions;

public class GroupsException : CustomException {
    public GroupsException(string where , string code , string description) : base(where , code , description) {
    }
}
