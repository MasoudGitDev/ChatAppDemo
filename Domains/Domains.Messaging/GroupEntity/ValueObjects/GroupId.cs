using Shared.ValueObjects;
namespace Domains.Messaging.GroupEntity.ValueObjects {
    public record GroupId : EntityId {
        public GroupId(Guid id):base(id,"Groups") { }           
    }
}
