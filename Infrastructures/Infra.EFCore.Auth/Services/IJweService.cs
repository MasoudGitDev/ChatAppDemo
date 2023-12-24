using Shared.Models;
namespace Infra.EfCore.Auth.Services;
public interface IJweService {
    Task<AccountResult> GenerateTokenAsync(string userIdentifier);
    Task<Dictionary<string,string>> GetClaimsByTokenAsync(string jweToken);
    Task<string> GetUserIdentifierClaimByTokenAsync(string jweToken);
}
