using Shared.Exceptions;
namespace Domains.Messaging.GroupEntity.Exceptions;
public class GroupCommandException : CustomException {
    public GroupCommandException(string where , string code , string description) : base(where , code , description) {
    }
}
