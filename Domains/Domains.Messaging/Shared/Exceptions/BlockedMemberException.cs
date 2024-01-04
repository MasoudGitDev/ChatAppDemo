using Shared.Exceptions;

namespace Domains.Messaging.Shared.Exceptions;
internal class BlockedMemberException : CustomException
{
    public BlockedMemberException(string where, string code, string description) : base(where, code, description)
    {
    }
}
