using Infra.EfCore.Auth.Constants;
using Infra.EfCore.Auth.Exceptions;
using Infra.EfCore.Auth.Extensions;
using Jose;
using Microsoft.Extensions.Configuration;
using Shared.Models;
using Shared.SystemModels;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Infra.EfCore.Auth.Services;
internal class JweService(IConfiguration configuration) : IJweService { 
    public Task<AccountResult> GenerateTokenAsync(Guid userIdentifier) {
        var jwtSettings = configuration.GetJweSettings();
        var claims = new Dictionary<string, string>()
        {
            { JweTypes.UserIdentifier, userIdentifier.ToString() },
            { JweTypes.TokenId ,  Guid.NewGuid().ToString()},
            { "aud", jwtSettings.Audience },
            { "iss", jwtSettings.Issuer },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
            { "exp", DateTimeOffset.UtcNow.AddMinutes(jwtSettings.ExpireAfterMinute).ToUnixTimeSeconds().ToString() }
        };
        var secretKey =Encoding.UTF8.GetBytes(jwtSettings.SecretKey.PadRight(64, '0'));
        string token = JWT.Encode(claims, secretKey, JweAlgorithm.DIR, JweEncryption.A256CBC_HS512);
        return Task.FromResult(new AccountResult(token,claims));
    }


    public Task<Dictionary<string,string>> GetClaimsByTokenAsync(string jweToken) {
        var secretKey = Encoding.UTF8.GetBytes(configuration.GetJweSettings().SecretKey.PadRight(64,'0')); 
        string payload = JWT.Decode(jweToken, secretKey , JweAlgorithm.DIR , JweEncryption.A256CBC_HS512);
        var claims = JsonSerializer.Deserialize<Dictionary<string,string>>(payload);
        if(claims is null ) { throw new JweException("GetClaimsByTokenAsync" , "NullObj" , "The <claims> can not be null."); }
        return Task.FromResult(claims);
    }
    public async Task<string> GetUserIdentifierClaimByTokenAsync(string jweToken) {
        var userIdentifierValue = (await GetClaimsByTokenAsync(jweToken)).FirstOrDefault(x=>x.Key == JweTypes.UserIdentifier).Value.ToString();
        if(String.IsNullOrWhiteSpace(userIdentifierValue)) {
            throw new JweException("GetUserIdentifierClaimByTokenAsync" , "NullOrWhitespace" , "<JwtTypes.UserIdentifier> value can not be null or empty or whitespace.");
        }
        return userIdentifierValue;    
    }
    private Task ValidateClaims(Dictionary<string , string> claims , JweSettingsModel model) {

        return Task.CompletedTask;


    }
}