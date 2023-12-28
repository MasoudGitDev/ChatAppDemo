using Shared.Exceptions;

namespace Shared.ValueObjects.Exceptions;
internal class EntityIdValueObjException : CustomException {
    public EntityIdValueObjException(string where , string code , string description) : base(where , code , description) {
    }
}
