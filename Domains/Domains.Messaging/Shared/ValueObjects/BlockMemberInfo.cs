using Domains.Messaging.Shared.Exceptions;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Domains.Messaging.Shared.ValueObjects;
public class BlockedMemberInfo {

    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public EntityId AdminId { get; set; }
    public string? Reason { get; set; }

    public BlockedMemberInfo() { }

    public BlockedMemberInfo(DateTime startAt , DateTime? endAt , EntityId adminId , string? reason) {

        if(endAt != null && endAt <= startAt) {
            throw new BlockedMemberException("Constructor" , "DateTimeEquality" , "The <endAt> date time must be grater the the <startAt> date time.");
        }
        if(adminId.Value == Guid.Empty) {
            throw new BlockedMemberException("Constructor" , "WrongGUID" , "The <adminId> must be a guid.");
        }
        StartAt = startAt;
        EndAt = endAt;
        AdminId = adminId;
        Reason = reason;
    }

    public static implicit operator string?(BlockedMemberInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator BlockedMemberInfo(string? jsonSource)
        => jsonSource.FromJsonTo<BlockedMemberInfo>();

}
