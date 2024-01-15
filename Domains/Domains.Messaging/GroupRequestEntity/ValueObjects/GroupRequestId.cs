using Shared.ValueObjects.Exceptions;
namespace Domains.Messaging.GroupRequestEntity.ValueObjects;
public record GroupRequestId {
    public Guid Value { get; }
    public GroupRequestId() {
        Value = Guid.NewGuid();
    }
    public GroupRequestId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException($"GroupRequestId-Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace.");
        }
        Value = id;
    }
    public static implicit operator Guid(GroupRequestId groupRequestId) => groupRequestId.Value;
    public static implicit operator GroupRequestId(Guid id) => new(id);
}
