using Shared.Exceptions;
namespace Apps.Messaging.Exceptions;
public class GroupAdminsException : CustomException {
    public GroupAdminsException(string where , string code , string description) : base(where , code , description) {
    }
}