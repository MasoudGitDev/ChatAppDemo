using Infra.EfCore.Auth.Exceptions;
using Microsoft.Extensions.Configuration;
using Shared.Enums;
using Shared.SystemModels;

namespace Infra.EfCore.Auth.Extensions;
public static class IConfigurationExtensions {
    public static AuthTokenSettingsModel GetJweSettings(this IConfiguration configuration) {
        var jwtSettings = configuration.GetSection(AppSettingNames.JweSettings).Get<AuthTokenSettingsModel>();
        if(jwtSettings is null) {
            throw new JweException("NullObj" , $"The <{nameof(AuthTokenSettingsModel)}> can not be null." );
        }
        return jwtSettings;
    }
}
