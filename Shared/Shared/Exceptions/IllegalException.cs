using Shared.Models;

namespace Shared.Exceptions;
internal class IllegalException : CustomException {
    public IllegalException() : base() { }

    public IllegalException(string message) : base(message) {
    }

    public IllegalException(string code , string description) : base(code , description) {
    }
}
