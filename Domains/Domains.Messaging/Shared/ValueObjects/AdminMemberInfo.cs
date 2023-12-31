using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Domains.Messaging.Shared.ValueObjects;
public record AdminMemberInfo
{
    public AdminAccessLevels AccessLevel { get; set; } = AdminAccessLevels.Low;
    public DateTime StartAdminAt { get; init; }
    public DateTime? EndAdminAt { get; set; }
    public EntityId ByWhomId { get; set; }
    public string? Reason { get; set; }

    public AdminMemberInfo() { }

    public AdminMemberInfo(AdminAccessLevels accessLevel , DateTime startAdminAt , DateTime? endAdminAt ,
                     EntityId byWhomId ,
                     string? reason) {
        AccessLevel = accessLevel;
        StartAdminAt = startAdminAt;
        EndAdminAt = endAdminAt;
        ByWhomId = byWhomId;
        Reason = reason;
    }

    public static implicit operator string(AdminMemberInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator AdminMemberInfo(string jsonSource)
        => jsonSource.FromJsonTo<AdminMemberInfo>() ?? new AdminMemberInfo();

}
