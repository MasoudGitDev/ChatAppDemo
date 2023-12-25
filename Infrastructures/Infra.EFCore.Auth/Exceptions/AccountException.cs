using Shared.Exceptions;

namespace Infra.EfCore.Auth.Exceptions;
internal class AccountException : CustomException {
    public AccountException(string where , string code , string description) : base(where , code , description) {
    }
}
