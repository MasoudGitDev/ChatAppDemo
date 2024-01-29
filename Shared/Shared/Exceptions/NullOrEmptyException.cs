using Shared.Models;

namespace Shared.Exceptions;
internal class NullOrEmptyException : CustomException {
    public NullOrEmptyException() {
    }

    public NullOrEmptyException(string message) : base(message) {
    }

    public NullOrEmptyException(ExceptionModel model) : base(model) {
    }

    public NullOrEmptyException(string code , string description) : base(code , description) {
    }
}
