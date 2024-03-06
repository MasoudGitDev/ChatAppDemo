
using Shared.ValueObjects.Exceptions;

namespace Shared.ValueObjects;

public interface IAuthToken {
    public string Value { get; }
}
public class JwtToken : IAuthToken {
    public string Value { get; }

    public JwtToken(string authToken) {
        if(String.IsNullOrWhiteSpace(authToken))
            throw new AuthTokenException("The <authToken> value can not be <NullOrWhiteSpace>.");
        if(Check2Dots(authToken) is false)
            throw new AuthTokenException("The <authToken> value is invalid");
        Value = authToken;
    }
    private JwtToken() {
        Value = string.Empty;
    }

    public static JwtToken Empty { get => new(); }

    private bool Check2Dots(string token) => token.Count(dot => dot == '.') == 2;

    public static implicit operator JwtToken(string token) => new(token);
    public static implicit operator string(JwtToken token) => token.Value;

}
