using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.Shared.Exceptions;
internal class BlockedMemberException : CustomException {
    public BlockedMemberException() {
    }

    public BlockedMemberException(string message) : base(message) {
    }

    public BlockedMemberException(ExceptionModel model) : base(model) {
    }

    public BlockedMemberException(string code , string description) : base(code , description) {
    }
}
