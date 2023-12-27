using Shared.Exceptions;

namespace Infra.EFCore.Repositories.Messaging.Exceptions;  
internal class GroupRepoException : CustomException {
    public GroupRepoException(string where , string code , string description) : base(where , code , description) {
    }
}
