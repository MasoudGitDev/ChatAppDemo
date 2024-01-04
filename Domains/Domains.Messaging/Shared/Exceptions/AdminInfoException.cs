using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class AdminInfoException : CustomException
{
    public AdminInfoException(string where, string code, string description) : base(where, code, description)
    {
    }
}
