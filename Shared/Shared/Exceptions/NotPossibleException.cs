using Shared.Models;

namespace Shared.Exceptions;
public class NotPossibleException : CustomException {   
    public NotPossibleException(string description) : base("NotPossible" , description) {
    }
}
