using Shared.Exceptions;

namespace Server.WebAPI.Controllers.Messaging.Exceptions {
    public class GroupControllerException : CustomException {
        public GroupControllerException(string where , string code , string description) : base(where , code , description) {
        }
    }
}
