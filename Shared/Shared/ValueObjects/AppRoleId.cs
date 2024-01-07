using Shared.ValueObjects;
namespace Domains.Messaging.Shared.ValueObjects;  
public record AppRoleId :EntityId {
    public AppRoleId(Guid id) : base(id , "AspNetRoles") {
    }
    public static implicit operator AppRoleId(Guid id) => new(id);
}
