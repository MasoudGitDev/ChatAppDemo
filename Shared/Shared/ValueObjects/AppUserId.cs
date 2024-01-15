using Shared.ValueObjects;
namespace Domains.Messaging.Shared.ValueObjects;  
public record AppUserId:EntityId {
    public AppUserId():base(){}
    public AppUserId(Guid id) : base(id) {}
    public static implicit operator AppUserId(Guid id) => new(id);
}