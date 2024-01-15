using Shared.ValueObjects.Exceptions;
namespace Shared.ValueObjects;
public record EntityId {
    public Guid Value { get; }

    public EntityId() {
        Value = Guid.NewGuid();
    }

    public EntityId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException($"Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace.");
        }
        Value = id;
    }
    public static implicit operator Guid(EntityId entityId) => entityId.Value;
    public static implicit operator EntityId(Guid id) => new(id);
}