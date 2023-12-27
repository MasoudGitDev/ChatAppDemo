using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class MemberValueObjException : CustomException {
    public MemberValueObjException(string where , string code , string description) : base(where , code , description) {
    }
}
