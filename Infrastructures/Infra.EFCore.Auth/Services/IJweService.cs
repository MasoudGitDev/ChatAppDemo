using Shared.Models;
using Shared.ValueObjects;
namespace Infra.EfCore.Auth.Services;
public interface IJweService {
    Task<AccountResult> GenerateTokenAsync(EntityId userIdentifier);
    Task<Dictionary<string,string>> GetClaimsByTokenAsync(string jweToken);
}
