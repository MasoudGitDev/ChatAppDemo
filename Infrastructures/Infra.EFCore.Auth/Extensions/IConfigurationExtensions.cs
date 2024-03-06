using Microsoft.Extensions.Configuration;
using Shared.Enums;
using Shared.Extensions;
using Shared.SystemModels;

namespace Infra.EfCore.Auth.Extensions;
public static class IConfigurationExtensions {
    public static AuthTokenSettingsModel GetAuthTokenSettingsModel(this IConfiguration configuration) {        
        return configuration
            .ThrowIfNull("configuration is null.")
            .GetSection(AppSettingNames.AuthTokenSettingsModel)
            .Get<AuthTokenSettingsModel>()
            .ThrowIfNull("AuthTokenSettingsModel is null.");
    }
}
