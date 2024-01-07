using Shared.ValueObjects;
namespace Domains.Messaging.GroupRequestEntity.ValueObjects;
public record GroupRequestId : EntityId {
    public GroupRequestId(Guid id) : base(id , "GroupRequests") {}
    public static implicit operator GroupRequestId(Guid id) => new GroupRequestId(id);
}
