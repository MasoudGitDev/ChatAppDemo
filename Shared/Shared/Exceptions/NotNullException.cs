using Shared.Models;

namespace Shared.Exceptions;

public class NotNullException : CustomException {
    public NotNullException() {
    }

    public NotNullException(string message) : base(message) {
    }

    public NotNullException(ExceptionModel model) : base(model) {
    }

    public NotNullException(string code , string description) : base(code , description) {
    }
}
