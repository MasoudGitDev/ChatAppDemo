using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.Shared.Exceptions;
internal class LogoException : CustomException {
    public LogoException() {
    }

    public LogoException(string message) : base(message) {
    }

    public LogoException(ExceptionModel model) : base(model) {
    }

    public LogoException(string code , string description) : base(code , description) {
    }
}
