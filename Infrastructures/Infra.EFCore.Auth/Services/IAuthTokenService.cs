using Domains.Messaging.Shared.ValueObjects;
using Shared.Models;
using Shared.ValueObjects;
namespace Infra.EfCore.Auth.Services;
public interface IAuthTokenService {
    Task<AccountResult> GenerateTokenAsync(AppUserId userId);
    Task<Dictionary<string , string>> GetClaimsByTokenAsync(IAuthToken authToken);
}
