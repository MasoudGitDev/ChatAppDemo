using Apps.Auth.Accounts.Repos;
using Infra.EfCore.Auth.Repos;
using Infra.EfCore.Auth.Services;
using Infra.EFCore.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EfCore.Auth.Extensions;
public static class IServiceCollectionExtensions {
    public static void Add_Infra_Auth_Services(this IServiceCollection services) {
        services.AddScoped<IAuthTokenService , JwtService>();
        services.AddScoped<IAccountRepo , AccountRepo>();
    }
}
