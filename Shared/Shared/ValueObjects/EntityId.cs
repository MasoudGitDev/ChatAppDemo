using Shared.ValueObjects.Exceptions;

namespace Shared.ValueObjects;  
public record EntityId {
    public Guid Value { get; }
    public string EntityName { get;}
    public EntityId(Guid id , string entityName = "Entity") {
        if(String.IsNullOrWhiteSpace(entityName)) {
            throw new EntityIdValueObjException("Constructor" , "NullOrWhiteSpace" , "The <entityName> can not be NullOrWhiteSpace.");
        }
        if(id == Guid.Empty || String.IsNullOrWhiteSpace(id.ToString())) {
            throw new EntityIdValueObjException($"Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace.");
        }
        Value = id;
        EntityName = entityName;
    }
    public static implicit operator Guid(EntityId id) => id.Value;
    public static implicit operator EntityId(Guid id) => new(id);
}
