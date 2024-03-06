using Shared.ValueObjects;
namespace Domains.Messaging.Shared.ValueObjects;  
public record AppUserId:EntityId {
    public AppUserId():base(){}
    public AppUserId(Guid id) : base(id) {}
    public AppUserId(string id) : base(id) { }
    public AppUserId(EntityId entityId):this(entityId.Value)
    {
       
    }

    public static implicit operator AppUserId(Guid id) => new(id);
    public static implicit operator AppUserId(string id) => new(id);
}