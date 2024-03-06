using Domains.Messaging.Shared.Exceptions;
using Shared.Extensions;

namespace Domains.Messaging.Shared.ValueObjects;
public record BlockedMemberInfo {

    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public AppUserId AdminId { get; set; } = "<unknown-id>";
    public string? Reason { get; set; }

    private BlockedMemberInfo() { }

    private BlockedMemberInfo(DateTime? startAt , DateTime? endAt , AppUserId adminId , string? reason) {
        if(endAt != null && startAt != null && endAt < startAt.Value.AddMinutes(1) ) {
            throw new BlockedMemberInfoException("DateTimeError" , "The <endAt> value must be atleast 1 min greater than the <startAt> value.");
        }
        StartAt = startAt ?? DateTime.UtcNow;
        EndAt = endAt; // null == forever.
        AdminId = adminId;
        Reason = reason;
    }

    public static BlockedMemberInfo Create(DateTime? startAt , DateTime? endAt , AppUserId adminId , string? reason)
        => new(startAt , endAt , adminId , reason);

    public static implicit operator string?(BlockedMemberInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator BlockedMemberInfo(string? jsonSource)
        => jsonSource.FromJsonTo<BlockedMemberInfo>();

}
