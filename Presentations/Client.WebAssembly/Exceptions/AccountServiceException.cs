using Shared.Exceptions;
using Shared.Models;

namespace Client.WebAssembly.Exceptions;
public class AccountServiceException : CustomException {
    public AccountServiceException() {
    }

    public AccountServiceException(string message) : base(message) {
    }

    public AccountServiceException(ExceptionModel model) : base(model) {
    }

    public AccountServiceException(string code , string description) : base(code , description) {
    }
}
