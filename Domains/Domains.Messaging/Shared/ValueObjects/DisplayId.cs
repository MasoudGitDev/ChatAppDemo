using Domains.Messaging.Shared.Exceptions;
namespace Domains.Messaging.Shared.ValueObjects;  
public record DisplayId {
    public string Value { get;}
    public DisplayId(string displayId)
    {
        if(String.IsNullOrWhiteSpace(displayId)) {
            throw new DisplayIdException("NullOrWhiteSpace" , "The <displayId> can not be NullOrWhiteSpace.");
        }
        Value = displayId;
    }
    public static implicit operator DisplayId(string displayId) => new DisplayId(displayId);
    public static implicit operator string(DisplayId displayId) => displayId.Value;
}
