using Shared.Exceptions;

namespace Shared.ValueObjects.Exceptions;
public class EntityIdException : CustomException {
    public EntityIdException(string where , string code , string description) : base(where , code , description) {
    }
}
