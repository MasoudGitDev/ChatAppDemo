using Shared.Models;
using Shared.ValueObjects.Exceptions;
namespace Shared.ValueObjects;
public record EntityId {
    public Guid Value { get; }

    public EntityId() {
        Value = Guid.NewGuid();
    }

    public EntityId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException(new ExceptionModel(nameof(EntityId) , "Constructor" , "NullOrWhiteSpace" ,
                "The <id> can not be NullOrWhiteSpace."));
        }
        Value = id;
    }
    public EntityId(string id) {
        if(String.IsNullOrWhiteSpace(id)) {
            throw new EntityIdException(new ExceptionModel(nameof(EntityId) , "Constructor" , "NullOrWhiteSpace" ,
                "The <id> can not be NullOrWhiteSpace."));
        }
        if(!Guid.TryParse(id , out Guid result)) {
            throw new EntityIdException($"Can not convert id :{id} to Guid.");
        }
        Value = result;
    }

    public static implicit operator string(EntityId entityId) => entityId.Value.ToString();
    public static implicit operator Guid(EntityId entityId) => entityId.Value;
    public static implicit operator EntityId(Guid id) => new(id);
    public static implicit operator EntityId(string id) => new(id);

}