using Infra.EfCore.Auth.Constants;
using Infra.EfCore.Auth.Exceptions;
using Infra.EfCore.Auth.Extensions;
using Infra.EfCore.Auth.Services;
using Jose;
using Microsoft.Extensions.Configuration;
using Shared.Models;
using Shared.ValueObjects;
using System.Text;
using System.Text.Json;

namespace Infra.EFCore.Auth.Services;
internal class JwtService(IConfiguration configuration) : IAuthTokenService {
    public Task<AccountResult> GenerateTokenAsync(EntityId userIdentifier) {
        var jwtSettings = configuration.GetJweSettings();
        var claims = new Dictionary<string, string>()
        {
            { JweTypes.UserIdentifier, userIdentifier.Value.ToString() },
            { JweTypes.TokenId ,  Guid.NewGuid().ToString()},
            { "aud", jwtSettings.Audience },
            { "iss", jwtSettings.Issuer },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
            { "exp", DateTimeOffset.UtcNow.AddMinutes(jwtSettings.ExpireAfterMinute).ToUnixTimeSeconds().ToString() },
            // Add any additional claims here
        };
        var secretKey =Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        // Change the algorithm and encryption parameters here
        string token = JWT.Encode(claims, secretKey, JwsAlgorithm.HS256);
        return Task.FromResult(new AccountResult(token , claims));
    }


    public Task<Dictionary<string , string>> GetClaimsByTokenAsync(string jwtToken) {
        var secretKey = Encoding.UTF8.GetBytes(configuration.GetJweSettings().SecretKey);
        // Change the algorithm and encryption parameters here
        string payload = JWT.Decode(jwtToken, secretKey , JwsAlgorithm.HS256);
        var claims = JsonSerializer.Deserialize<Dictionary<string,string>>(payload);
        if(claims is null) { throw new JweException("NullObj" , "The <claims> can not be null."); }
        return Task.FromResult(claims);
    }
}
