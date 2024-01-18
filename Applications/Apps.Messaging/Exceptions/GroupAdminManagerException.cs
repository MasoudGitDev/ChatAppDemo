using Shared.Exceptions;
namespace Apps.Messaging.Exceptions {
    internal class GroupAdminManagerException : CustomException {
        public GroupAdminManagerException(string where , string code , string description) : base(where , code , description) {
        }
    }
}
