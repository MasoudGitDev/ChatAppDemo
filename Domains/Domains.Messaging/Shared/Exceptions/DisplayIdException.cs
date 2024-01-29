using Shared.Exceptions;
using Shared.Models;

namespace Domains.Messaging.Shared.Exceptions;
internal class DisplayIdException : CustomException {
    public DisplayIdException() {
    }

    public DisplayIdException(string message) : base(message) {
    }

    public DisplayIdException(ExceptionModel model) : base(model) {
    }

    public DisplayIdException(string code , string description) : base(code , description) {
    }
}
