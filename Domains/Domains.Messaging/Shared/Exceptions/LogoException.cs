using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class LogoException : CustomException {
    public LogoException(string where , string code , string description) : base(where , code , description) {
    }
}
