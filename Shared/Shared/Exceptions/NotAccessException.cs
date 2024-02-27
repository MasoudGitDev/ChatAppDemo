using Shared.Models;

namespace Shared.Exceptions;
public class NotAccessException : CustomException {   
    public NotAccessException(string description) : base( "NotAccess" , description) {
    }
}
