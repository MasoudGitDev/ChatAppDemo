using Shared.Exceptions;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging.Exceptions {
    public class GroupControllerException : CustomException {
        public GroupControllerException() {
        }

        public GroupControllerException(string message) : base(message) {
        }

        public GroupControllerException(ExceptionModel model) : base(model) {
        }

        public GroupControllerException(string code , string description) : base(code , description) {
        }
    }
}
