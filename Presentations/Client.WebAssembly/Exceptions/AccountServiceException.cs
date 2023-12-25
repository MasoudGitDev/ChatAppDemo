using Shared.Exceptions;

namespace Client.WebAssembly.Exceptions;  
public class AccountServiceException : CustomException {
    public AccountServiceException(string where , string code , string description) : base(where , code , description) {
    }
}
