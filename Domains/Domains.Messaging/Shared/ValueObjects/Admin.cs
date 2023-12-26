using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;

namespace Domains.Messaging.Shared.ValueObjects;
public class Admin
{
    public string UserId { get; init; } = string.Empty;
    public AdminAccessLevels AccessLevel { get; set; } = AdminAccessLevels.Low;
    public DateTime AsAdminAt { get; init; }
    public bool IsOnline { get; set; } = false;

    public Admin() { }

    public Admin(string userId, AdminAccessLevels accessLevel , DateTime asAdminAt)
    {
        UserId = userId;
        AccessLevel = accessLevel;
        AsAdminAt = asAdminAt;
    }

    public static implicit operator string(Admin groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator Admin(string jsonSource)
        => jsonSource.FromJsonTo<Admin>() ?? new Admin();

}
