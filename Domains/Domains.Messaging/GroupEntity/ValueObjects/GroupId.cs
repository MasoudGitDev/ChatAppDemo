using Shared.Models;
using Shared.ValueObjects.Exceptions;
namespace Domains.Messaging.GroupEntity.ValueObjects;  
public record GroupId  {
    public Guid Value { get;}
    public GroupId()  { 
        Value = Guid.NewGuid();
    }
    public GroupId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException(new ExceptionModel("GroupId","Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace."));
        }
        Value = id;
    }
    public GroupId(string id) {
        if(String.IsNullOrWhiteSpace(id)) {
            throw new EntityIdException(new ExceptionModel("GroupId" , "Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace."));
        }
        if(!Guid.TryParse(id , out Guid result)) {
            throw new EntityIdException(new ExceptionModel("GroupId" , "Constructor" , "StringToGUID" , "This string <id> can not convert to guid."));
        }
        Value = result;
    }

    public static GroupId Create() => new GroupId();

    public static implicit operator Guid(GroupId groupId) => groupId.Value;
    public static implicit operator GroupId(Guid id) => new(id);
    public static implicit operator GroupId(string id) => new(id);
    public static implicit operator string(GroupId groupId) => groupId.Value.ToString();
}
