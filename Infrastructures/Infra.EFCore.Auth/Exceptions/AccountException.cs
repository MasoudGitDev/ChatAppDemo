using Shared.Exceptions;
using Shared.Models;

namespace Infra.EfCore.Auth.Exceptions;
internal class AccountException : CustomException {
    public AccountException() {
    }

    public AccountException(string message) : base(message) {
    }

    public AccountException(ExceptionModel model) : base(model) {
    }

    public AccountException(string code , string description) : base(code , description) {
    }
}
