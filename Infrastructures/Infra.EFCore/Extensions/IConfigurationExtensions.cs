using Microsoft.Extensions.Configuration;
using Shared.Enums;
using Shared.Extensions;
using Shared.SystemModels;

namespace Infra.EFCore.Extensions;
internal static class IConfigurationExtensions {
    public static AuthTokenSettingsModel GetAuthTokenSettings(this IConfiguration configuration) {
        var authTokenSettings = configuration.GetSection(AppSettingNames.AuthTokenSettingsModel).Get<AuthTokenSettingsModel>();
        if(authTokenSettings == null) {
            throw new ArgumentNullException("AuthTokenSettingsModel is null.");
        }
        return authTokenSettings;
    }
    public static string GetDefaultConnectionString(this IConfiguration configuration) {
        return configuration.GetConnectionString(ConnectionNames.Default)
            .ThrowIfNullOrWhiteSpace("System can not extract the default connection string.");
    }
    public static string ChooseConnectionString(this IConfiguration configuration , ConnectionNames connectionName) {
        var strConn = configuration.GetConnectionString(connectionName.ToString()!);
        if(string.IsNullOrEmpty(strConn)) {
            throw new ArgumentNullException($"<{connectionName}> connection string is null or empty.");
        }
        return strConn;
    }
}
