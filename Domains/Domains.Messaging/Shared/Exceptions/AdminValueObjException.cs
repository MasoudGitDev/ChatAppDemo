using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class AdminValueObjException : CustomException
{
    public AdminValueObjException(string where, string code, string description) : base(where, code, description)
    {
    }
}
