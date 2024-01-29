using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.Shared.Exceptions;
internal class AdminInfoException : CustomException {
    public AdminInfoException() {
    }

    public AdminInfoException(string message) : base(message) {
    }

    public AdminInfoException(ExceptionModel model) : base(model) {
    }

    public AdminInfoException(string code , string description) : base(code , description) {
    }
}
