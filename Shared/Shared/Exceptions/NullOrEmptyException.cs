using Shared.Models;

namespace Shared.Exceptions;
internal class NullOrEmptyException : CustomException {
    public NullOrEmptyException() {
    }

    public NullOrEmptyException(ExceptionModel model) : base(model) {
    }

    public NullOrEmptyException(string message) : base(message) {
    }

    public NullOrEmptyException(string where , string code , string message) : base(where , code , message) {
    }
}
