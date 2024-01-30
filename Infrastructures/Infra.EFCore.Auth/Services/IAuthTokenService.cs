using Shared.Models;
using Shared.ValueObjects;
namespace Infra.EfCore.Auth.Services;
public interface IAuthTokenService {
    Task<AccountResult> GenerateTokenAsync(EntityId userIdentifier);
    Task<Dictionary<string,string>> GetClaimsByTokenAsync(string authToken);
}
