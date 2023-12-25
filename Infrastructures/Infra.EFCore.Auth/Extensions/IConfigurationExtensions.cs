using Infra.EfCore.Auth.Exceptions;
using Microsoft.Extensions.Configuration;
using Shared.Enums;
using Shared.SystemModels;

namespace Infra.EfCore.Auth.Extensions;
public static class IConfigurationExtensions {
    public static JweSettingsModel GetJweSettings(this IConfiguration configuration) {
        var jwtSettings = configuration.GetSection(AppSettingNames.JweSettings).Get<JweSettingsModel>();
        if(jwtSettings is null) {
            throw new JweException(nameof(GetJweSettings) , "NullObj" , $"The <{nameof(JweSettingsModel)}> can not be null." );
        }
        return jwtSettings;
    }
}
