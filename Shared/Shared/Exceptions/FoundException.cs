using Shared.Models;

namespace Shared.Exceptions;

public class FoundException : CustomException {
    public FoundException() {
    }

    public FoundException(string message) : base(message) {
    }

    public FoundException(ExceptionModel model) : base(model) {
    }

    public FoundException(string code , string description) : base(code , description) {
    }
}
