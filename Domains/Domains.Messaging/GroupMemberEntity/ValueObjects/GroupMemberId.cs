using Shared.ValueObjects;
namespace Domains.Messaging.GroupMemberEntity.ValueObjects {
    public record GroupMemberId : EntityId {
        public GroupMemberId(Guid id) : base(id , "GroupMembers") {}
        public static implicit operator GroupMemberId(Guid id) => new(id);
    }
}
