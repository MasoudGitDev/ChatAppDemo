using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class GroupIdException : CustomException
{
    public GroupIdException(string where, string code, string description) : base(where, code, description)
    {
    }
}
