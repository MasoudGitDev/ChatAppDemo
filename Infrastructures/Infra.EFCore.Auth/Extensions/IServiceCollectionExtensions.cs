using Apps.Auth.Accounts.Repos;
using Infra.EfCore.Auth.Repos;
using Infra.EfCore.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EfCore.Auth.Extensions;
public static class IServiceCollectionExtensions {
    public static void Add_EFCore_Auth_Services(this IServiceCollection services) {
        services.AddScoped<IJweService , JweService>();
        services.AddScoped<IAccountRepo , AccountRepo>();
    }
}
