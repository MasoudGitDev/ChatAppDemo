using Shared.ValueObjects;
namespace Domains.Messaging.Shared.ValueObjects;  
public record AppRoleId :EntityId {
    public AppRoleId():base(){}
    public AppRoleId(Guid id) : base(id) {}
    public static implicit operator AppRoleId(Guid id) => new(id);
}
