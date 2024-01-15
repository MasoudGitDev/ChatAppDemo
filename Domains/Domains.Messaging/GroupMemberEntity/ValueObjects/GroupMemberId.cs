using Shared.ValueObjects.Exceptions;
namespace Domains.Messaging.GroupMemberEntity.ValueObjects;  
public record GroupMemberId {
    public Guid Value { get; }
    public GroupMemberId() {
        Value = Guid.NewGuid();
    }
    public GroupMemberId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException($"GroupMemberId-Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace.");
        }
        Value = id;
    }
    public static implicit operator Guid(GroupMemberId groupMemberId) => groupMemberId.Value;
    public static implicit operator GroupMemberId(Guid id) => new(id);
}
