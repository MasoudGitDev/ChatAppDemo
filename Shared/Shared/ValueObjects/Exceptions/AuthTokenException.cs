using Shared.Exceptions;
using Shared.Models;

namespace Shared.ValueObjects.Exceptions;
public class AuthTokenException : CustomException {
    public AuthTokenException() {
    }

    public AuthTokenException(string message) : base(message) {
    }

    public AuthTokenException(ExceptionModel model) : base(model) {
    }

    public AuthTokenException(string code , string description) : base(code , description) {
    }
  
}
