using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class LogoValueObjException : CustomException {
    public LogoValueObjException(string where , string code , string description) : base(where , code , description) {
    }
}
