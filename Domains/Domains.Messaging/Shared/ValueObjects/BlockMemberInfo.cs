using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Domains.Messaging.Shared.ValueObjects;
public class BlockMemberInfo {

    public DateTime StartBlockAt { get; set; }
    public DateTime? EndBlockAt { get; set; }
    public EntityId BlockedById { get; set; }
    public string? Reason { get; set; }

    public BlockMemberInfo() { }

    public BlockMemberInfo(DateTime startBlockAt , DateTime? endBlockAt , EntityId blockedById , string? reason) {
        StartBlockAt = startBlockAt;
        EndBlockAt = endBlockAt;
        BlockedById = blockedById;
        Reason = reason;
    }

    public static implicit operator string(BlockMemberInfo groupAdmin)
        => groupAdmin.ToJson();
    public static implicit operator BlockMemberInfo(string jsonSource)
        => jsonSource.FromJsonTo<BlockMemberInfo>() ?? new BlockMemberInfo();

}
