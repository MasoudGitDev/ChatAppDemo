using Shared.Exceptions;
using Shared.Models;

namespace Client.WebAssembly.Exceptions;
public class AuthStateProviderException : CustomException {
    public AuthStateProviderException() {
    }

    public AuthStateProviderException(string message) : base(message) {
    }

    public AuthStateProviderException(ExceptionModel model) : base(model) {
    }

    public AuthStateProviderException(string code , string description) : base(code , description) {
    }
}
