using Shared.Exceptions;

namespace Client.WebAssembly.Exceptions;  
public class AuthStateProviderException : CustomException {
    public AuthStateProviderException(string where , string code , string description) : base(where , code , description) {
    }
}
