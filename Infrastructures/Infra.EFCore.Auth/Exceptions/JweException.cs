using Shared.Exceptions;

namespace Infra.EfCore.Auth.Exceptions;
internal class JweException : CustomException {
    public JweException(string where , string code , string description) : base(where , code , description) {
    }
}
