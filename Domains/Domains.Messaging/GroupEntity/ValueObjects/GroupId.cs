using Domains.Messaging.Shared.Abstractions;
using Domains.Messaging.Shared.Exceptions;
namespace Domains.Messaging.GroupEntity.ValueObjects {
    public record GroupId {
        public Guid Value { get; }
        public GroupId(Guid id) {
            if(id == Guid.Empty || String.IsNullOrWhiteSpace(id.ToString())) {
                throw new GroupIdValueObjException($"Constructor" , "NullOrWhiteSpace" , $"The <{id}> can not be NullOrWhiteSpace.");
            }
            Value = id;
        }
        public static implicit operator Guid(GroupId id) => id.Value;
        public static implicit operator GroupId(Guid id) => new(id);
    }
}
