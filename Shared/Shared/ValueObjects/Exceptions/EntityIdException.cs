using Shared.Exceptions;
using Shared.Models;

namespace Shared.ValueObjects.Exceptions;
public class EntityIdException : CustomException {
    public EntityIdException() {
    }

    public EntityIdException(string message) : base(message) {
    }

    public EntityIdException(ExceptionModel model) : base(model) {
    }

    public EntityIdException(string code , string description) : base(code , description) {
    }
}
