using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Domains.Messaging.Shared.ValueObjects;
public class AdminInfo
{
    public AdminAccessLevels AccessLevel { get; set; } = AdminAccessLevels.Low;
    public DateTime AdminAt { get; init; }

    public EntityId AdminModifierId { get; set; }

    public AdminInfo() { }

    public AdminInfo(AdminAccessLevels accessLevel , DateTime adminAt , EntityId adminModifierId) {
        AccessLevel = accessLevel;
        AdminAt = adminAt;
        AdminModifierId = adminModifierId;
    }

    public static implicit operator string(AdminInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator AdminInfo(string jsonSource)
        => jsonSource.FromJsonTo<AdminInfo>() ?? new AdminInfo();

}
