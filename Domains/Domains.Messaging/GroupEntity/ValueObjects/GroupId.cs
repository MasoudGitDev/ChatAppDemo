using Shared.ValueObjects;
namespace Domains.Messaging.GroupEntity.ValueObjects;  
public record GroupId : EntityId {
    public GroupId(Guid id):base(id,"Groups") { }
    public static implicit operator GroupId(Guid id) => new(id);
}
