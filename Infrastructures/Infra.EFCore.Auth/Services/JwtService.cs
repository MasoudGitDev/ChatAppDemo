using Domains.Messaging.Shared.ValueObjects;
using Infra.EfCore.Auth.Constants;
using Infra.EfCore.Auth.Extensions;
using Infra.EfCore.Auth.Services;
using Jose;
using Microsoft.Extensions.Configuration;
using Shared.Extensions;
using Shared.Models;
using Shared.SystemModels;
using Shared.ValueObjects;
using System.Text;
using System.Text.Json;

namespace Infra.EFCore.Auth.Services;
internal class JwtService(IConfiguration configuration) : IAuthTokenService {
    //if you want to use jwe => JWT.Encode(claims, secretKey, JweAlgorithm.DIR, JweEncryption.A256CBC_HS512);
    //======================================== readOnly Fields
    private readonly JwsAlgorithm _algorithm = JwsAlgorithm.HS256;
    private readonly byte[] _getSecureKey = Encoding.UTF8.GetBytes(configuration.GetAuthTokenSettingsModel().SecretKey);

    //======================================== public functions
    public Task<AccountResult> GenerateTokenAsync(AppUserId userIdentifier) {
        var claims = CreateClaims(userIdentifier, configuration.GetAuthTokenSettingsModel());
        return Task.FromResult(new AccountResult(CreateTokenByClaims(claims) , KeyValueClaims: claims));
    }
    public Task<Dictionary<string , string>> GetClaimsByTokenAsync(IAuthToken authToken)
       => Task.FromResult(( JsonSerializer.Deserialize<Dictionary<string , string>>(Decode(authToken)) )
           .ThrowIfNullOrEmpty("System can not extract the claims from the authToken."));

    //======================================== private functions

    private Dictionary<string , string> CreateClaims(AppUserId userId , AuthTokenSettingsModel authSettingsModel)
        => new()
        {
            { JweTypes.UserId, userId.Value.ToString() },
            { JweTypes.TokenId ,  Guid.NewGuid().ToString()},
            { "aud", authSettingsModel.Audience },
            { "iss", authSettingsModel.Issuer },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
            { "exp", DateTimeOffset.UtcNow.AddMinutes(authSettingsModel.ExpireAfterMinute).ToUnixTimeSeconds().ToString() },
        };

    private IAuthToken CreateTokenByClaims(Dictionary<string , string> keyValueClaims)
      => new JwtToken(( JWT.Encode(keyValueClaims , _getSecureKey , _algorithm) ));

    private string Decode(IAuthToken authToken)
        => ( JWT.Decode(authToken.Value , _getSecureKey , _algorithm) )
        .ThrowIfNullOrWhiteSpace("Error:System can not decode the authToken value.");
}
