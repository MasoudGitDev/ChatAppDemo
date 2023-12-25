using Microsoft.Extensions.Configuration;
using Shared.Enums;
using Shared.SystemModels;

namespace Infra.EFCore.Extensions;  
internal static class IConfigurationExtensions {
    public static JweSettingsModel GetJweSettings(this IConfiguration configuration) {
        var jweSettings = configuration.GetSection(AppSettingNames.JweSettings).Get<JweSettingsModel>();
        if(jweSettings == null) {
            throw new ArgumentNullException("JwtSettingsModel is null.");
        }
        return jweSettings;
    }
    public static string GetDefaultConnectionString(this IConfiguration configuration) {
        var strConn = configuration.GetConnectionString(ConnectionNames.Default);
        if(string.IsNullOrEmpty(strConn)) {
            throw new ArgumentNullException("<Default> connection string is null or empty.");
        }
        return strConn;
    }
    public static string ChooseConnectionString(this IConfiguration configuration , ConnectionNames connectionName) {
        var strConn = configuration.GetConnectionString(connectionName.ToString()!);
        if(string.IsNullOrEmpty(strConn)) {
            throw new ArgumentNullException($"<{connectionName}> connection string is null or empty.");
        }
        return strConn;
    }
}
