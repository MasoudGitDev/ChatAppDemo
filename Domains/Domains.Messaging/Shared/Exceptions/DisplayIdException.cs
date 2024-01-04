using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class DisplayIdException : CustomException
{
    public DisplayIdException(string where, string code, string description) : base(where, code, description)
    {
    }
}
