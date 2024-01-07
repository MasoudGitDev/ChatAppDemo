using Shared.ValueObjects;
using Shared.ValueObjects.Exceptions;
namespace Domains.Messaging.Shared.ValueObjects;  
public record AppUserId:EntityId {
    public AppUserId(Guid id) : base(id , "AspNetUsers") {
    }
    public static implicit operator AppUserId(Guid id) => new(id);
}