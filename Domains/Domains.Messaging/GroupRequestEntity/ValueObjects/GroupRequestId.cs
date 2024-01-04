using Shared.ValueObjects;
namespace Domains.Messaging.GroupRequestEntity.ValueObjects;
public record GroupRequestId : EntityId {
    protected GroupRequestId(Guid id) : base(id , "GroupRequests") {}
}
