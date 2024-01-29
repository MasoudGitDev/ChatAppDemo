using Shared.Models;
using Shared.ValueObjects.Exceptions;

namespace Domains.Messaging.GroupMessageEntity.ValueObjects;  
public record GroupMessageId {
    public Guid Value { get; set; }
    public GroupMessageId()
    {
        Value = Guid.NewGuid();
    }
    public GroupMessageId(Guid id)
    {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException(new ExceptionModel("GroupMessageId", "Constructor" , "NotValidGUID" , ""));
        }
        Value = id;
    }
    public static implicit operator GroupMessageId(Guid id) => new GroupMessageId(id);
    public static implicit operator Guid(GroupMessageId id) => id.Value;
}
