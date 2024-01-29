using Shared.Exceptions;
using Shared.Models;
namespace Apps.Messaging.Exceptions {
    internal class GroupAdminManagerException : CustomException {
        public GroupAdminManagerException() {
        }

        public GroupAdminManagerException(string message) : base(message) {
        }

        public GroupAdminManagerException(ExceptionModel model) : base(model) {
        }

        public GroupAdminManagerException(string code , string description) : base(code , description) {
        }
    }
}
