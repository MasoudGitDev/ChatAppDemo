using Shared.Exceptions;
namespace Domains.Messaging.GroupEntity.Exceptions;
public class GroupException : CustomException {
    public GroupException(string where , string code , string description) : base(where , code , description) {
    }
}
