using Shared.Models;
using Shared.ValueObjects.Exceptions;
namespace Domains.Messaging.GroupRequestEntity.ValueObjects;
public record GroupRequestId {
    public Guid Value { get; }
    private GroupRequestId() {
        Value = Guid.NewGuid();
    }
    public GroupRequestId(Guid id) {
        if(String.IsNullOrWhiteSpace(id.ToString()) || id == Guid.Empty) {
            throw new EntityIdException(new ExceptionModel("GroupRequestId","Constructor" , "NullOrWhiteSpace" , "The <id> can not be NullOrWhiteSpace."));
        }
        Value = id;
    }
    public static GroupRequestId Create() => new GroupRequestId();
    public static implicit operator Guid(GroupRequestId groupRequestId) => groupRequestId.Value;
    public static implicit operator GroupRequestId(Guid id) => new(id);
}
