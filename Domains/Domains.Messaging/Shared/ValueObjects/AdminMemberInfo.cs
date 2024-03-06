using Domains.Messaging.Shared.Exceptions;
using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
namespace Domains.Messaging.Shared.ValueObjects;
public record AdminMemberInfo
{
    public AdminLevel AdminLevel { get; set; } = AdminLevel.Regular;
    public DateTime StartAt { get; init; }
    public DateTime? EndAt { get; set; }
    public Guid ByWhomId { get; set; }
    public string? Reason { get; set; }

    public AdminMemberInfo() { }

    public AdminMemberInfo(AdminLevel accessLevel , DateTime startAt , DateTime? endAt ,
                     Guid byWhomId ,
                     string? reason) {
        if(endAt != null && endAt <= startAt) {
            throw new AdminInfoException("DateTimeEquality" , "The <endAt> date time must be grater the the <startAt> date time.");
        }
        if(byWhomId == Guid.Empty) {
            throw new AdminInfoException("WrongGUID" , "The <byWhomId> must be a guid.");
        }
        AdminLevel = accessLevel;
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
