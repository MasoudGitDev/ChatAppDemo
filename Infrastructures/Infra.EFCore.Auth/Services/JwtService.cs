using Infra.EfCore.Auth.Constants;
using Infra.EfCore.Auth.Extensions;
using Infra.EfCore.Auth.Services;
using Jose;
using Microsoft.Extensions.Configuration;
using Shared.Extensions;
using Shared.Models;
using Shared.ValueObjects;
using System.Text;
using System.Text.Json;

namespace Infra.EFCore.Auth.Services;
internal class JwtService(IConfiguration configuration) : IAuthTokenService {
    private readonly JwsAlgorithm _algorithm = JwsAlgorithm.HS256;
    private readonly byte[] _getSecureKey = Encoding.UTF8.GetBytes(configuration.GetAuthSettings().SecretKey);

    public Task<AccountResult> GenerateTokenAsync(EntityId userIdentifier) {
        var authSettings = configuration.GetAuthSettings();
        var claims = new Dictionary<string, string>()
        {
            { JweTypes.UserIdentifier, userIdentifier.Value.ToString() },
            { JweTypes.TokenId ,  Guid.NewGuid().ToString()},
            { "aud", authSettings.Audience },
            { "iss", authSettings.Issuer },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
            { "exp", DateTimeOffset.UtcNow.AddMinutes(authSettings.ExpireAfterMinute).ToUnixTimeSeconds().ToString() },
        };

        return Task.FromResult(new AccountResult(
            AuthToken: ( JWT.Encode(claims , _getSecureKey , _algorithm) ).IfNullOrWhiteSpace("Error:System can not create authToken.") ,
            KeyValueClaims: claims));
    }

    public Task<Dictionary<string , string>> GetClaimsByTokenAsync(string authToken)
        => Task.FromResult(( JsonSerializer.Deserialize<Dictionary<string , string>>(Decode(authToken)) )
            .IfNullOrEmpty("System can not extract the claims from authToken."));
        


    private string Decode(string authToken) => JWT.Decode(authToken , _getSecureKey , _algorithm);

}
