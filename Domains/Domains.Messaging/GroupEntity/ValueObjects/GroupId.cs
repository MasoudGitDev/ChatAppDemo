using Shared.ValueObjects.Exceptions;
namespace Domains.Messaging.GroupEntity.ValueObjects;  
public record GroupId  {
    public Guid Value { get;}
    public GroupId()  { 
        Value = Guid.NewGuid();
    }
    public GroupId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException($"GroupId-Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace.");
        }
        Value = id;
    }
    public static implicit operator Guid(GroupId groupId) => groupId.Value;
    public static implicit operator GroupId(Guid id) => new(id);
}
