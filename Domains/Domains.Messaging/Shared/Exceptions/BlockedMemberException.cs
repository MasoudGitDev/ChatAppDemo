using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.Shared.Exceptions;
internal class BlockedMemberInfoException : CustomException {
    public BlockedMemberInfoException() {
    }

    public BlockedMemberInfoException(string message) : base(message) {
    }

    public BlockedMemberInfoException(ExceptionModel model) : base(model) {
    }

    public BlockedMemberInfoException(string code , string description) : base(code , description) {
    }
}
