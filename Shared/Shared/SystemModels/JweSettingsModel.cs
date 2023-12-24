namespace Shared.SystemModels;
public class JweSettingsModel {
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public byte ExpireAfterMinute { get; set; } = 0;
    public string SecretKey { get; set; } = string.Empty;
}
