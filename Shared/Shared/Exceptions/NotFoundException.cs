using Shared.Models;

namespace Shared.Exceptions;

public class NotFoundException : CustomException {
    public NotFoundException() {
    }

    public NotFoundException(string message) : base(message) {
    }

    public NotFoundException(ExceptionModel model) : base(model) {
    }

    public NotFoundException(string code , string description) : base(code , description) {
    }
}
