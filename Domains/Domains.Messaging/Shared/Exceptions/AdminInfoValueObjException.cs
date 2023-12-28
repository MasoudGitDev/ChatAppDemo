using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class AdminInfoValueObjException : CustomException
{
    public AdminInfoValueObjException(string where, string code, string description) : base(where, code, description)
    {
    }
}
