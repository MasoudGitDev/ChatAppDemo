using Shared.Models;

namespace Shared.Exceptions;
public class UnknownActionException : CustomException {
    public UnknownActionException() : base(
        "<unknown-action>" ,
        "System can not handle the action.") { }

    public UnknownActionException(string description) : base("<unknown-action>" , description) {
    }
}
