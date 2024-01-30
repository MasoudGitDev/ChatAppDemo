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

    //======================================== readOnly Fields
    private readonly JwsAlgorithm _algorithm = JwsAlgorithm.HS256;
    private readonly byte[] _getSecureKey = Encoding.UTF8.GetBytes(configuration.GetAuthSettings().SecretKey);

    //======================================== public functions
    public Task<AccountResult> GenerateTokenAsync(EntityId userIdentifier) {
        var claims = CreateClaims(userIdentifier, configuration.GetAuthSettings());
        return Task.FromResult(new AccountResult(CreateTokenByClaims(claims) , KeyValueClaims: claims));
    }
    public Task<Dictionary<string , string>> GetClaimsByTokenAsync(string authToken)
       => Task.FromResult(( JsonSerializer.Deserialize<Dictionary<string , string>>(Decode(authToken)) )
           .IfNullOrEmpty("System can not extract the claims from authToken."));

    //======================================== private functions

    private Dictionary<string , string> CreateClaims(EntityId userId , AuthTokenSettingsModel authSettingsModel)
        => new Dictionary<string , string>()
        {
            { JweTypes.UserIdentifier, userId.Value.ToString() },
            { JweTypes.TokenId ,  Guid.NewGuid().ToString()},
            { "aud", authSettingsModel.Audience },
            { "iss", authSettingsModel.Issuer },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
            { "exp", DateTimeOffset.UtcNow.AddMinutes(authSettingsModel.ExpireAfterMinute).ToUnixTimeSeconds().ToString() },
        };

    private string CreateTokenByClaims(Dictionary<string , string> keyValueClaims)
      => ( JWT.Encode(keyValueClaims , _getSecureKey , _algorithm) )
          .IfNullOrWhiteSpace("Error:System can not encode the claims.");

    private string Decode(string authToken)
        => ( JWT.Decode(authToken , _getSecureKey , _algorithm) )
        .IfNullOrWhiteSpace("Error:System can not decode authToken.");



}
