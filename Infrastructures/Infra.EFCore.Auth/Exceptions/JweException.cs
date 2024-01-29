using Shared.Exceptions;
using Shared.Models;

namespace Infra.EfCore.Auth.Exceptions;
internal class JweException : CustomException {
    public JweException() {
    }

    public JweException(string message) : base(message) {
    }

    public JweException(ExceptionModel model) : base(model) {
    }

    public JweException(string code , string description) : base(code , description) {
    }
}
