using Microsoft.Extensions.Configuration;
using Shared.SystemModels;

namespace Infra.EfCore.Auth.Extensions;
public static class IConfigurationExtensions {
    public static JweSettingsModel GetJwtSettings(this IConfiguration configuration) {
        var jwtSettings = configuration.GetSection("JWTSettings").Get<JweSettingsModel>();
        if(jwtSettings is null) {
            throw new ArgumentNullException("GetJwtSettings : <JwtSettingsModel> is null.");
        }
        return jwtSettings;
    }
}
