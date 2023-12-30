using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Domains.Messaging.Shared.ValueObjects;
public class BlockMemberInfo {

    public DateTime BlockedAt { get; set; }
    public EntityId BlockedBy { get; set; }
    public string? Description { get; set; }

    public BlockMemberInfo() { }

    public BlockMemberInfo(DateTime blockedAt , EntityId blockedBy , string? description) {
        BlockedAt = blockedAt;
        BlockedBy = blockedBy;
        Description = description;
    }

    public static implicit operator string(BlockMemberInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator BlockMemberInfo(string jsonSource)
        => jsonSource.FromJsonTo<BlockMemberInfo>() ?? new BlockMemberInfo();

}
