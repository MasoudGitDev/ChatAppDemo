using Domains.Messaging.Shared.Exceptions;
using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Domains.Messaging.Shared.ValueObjects;
public record AdminMemberInfo
{
    public AdminAccessLevels AccessLevel { get; set; } = AdminAccessLevels.Low;
    public DateTime StartAt { get; init; }
    public DateTime? EndAt { get; set; }
    public EntityId ByWhomId { get; set; }
    public string? Reason { get; set; }

    public AdminMemberInfo() { }

    public AdminMemberInfo(AdminAccessLevels accessLevel , DateTime startAt , DateTime? endAt ,
                     EntityId byWhomId ,
                     string? reason) {
        if(endAt != null && endAt <= startAt) {
            throw new AdminInfoException("Constructor" , "DateTimeEquality" , "The <endAt> date time must be grater the the <startAt> date time.");
        }
        if(byWhomId.Value == Guid.Empty) {
            throw new AdminInfoException("Constructor" , "WrongGUID" , "The <byWhomId> must be a guid.");
        }
        AccessLevel = accessLevel;
        StartAt = startAt;
        EndAt = endAt;
        ByWhomId = byWhomId;
        Reason = reason;
    }

    public static implicit operator string?(AdminMemberInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator AdminMemberInfo(string? jsonSource)
        => jsonSource.FromJsonTo<AdminMemberInfo>();

}
