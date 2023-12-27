using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class GroupIdValueObjException : CustomException
{
    public GroupIdValueObjException(string where, string code, string description) : base(where, code, description)
    {
    }
}
