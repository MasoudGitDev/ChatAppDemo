using Domains.Messaging.Shared.Exceptions;
using Shared.Extensions;

namespace Domains.Messaging.Shared.ValueObjects;  
public record Logo {
    public string Image { get; } = string.Empty;
    public string Title { get; set; }

    public Logo() { }
    public Logo(string image , string title = "")
    {
        if(String.IsNullOrWhiteSpace(image)) {
            throw new LogoValueObjException("Constructor" , "NullOrWhiteSpace" , "The <image> can not be NullOrWhiteSpace.");
        }
        Image = image;
        Title = title;
    }

    public static implicit operator Logo(string jsonSource) => jsonSource.FromJsonTo<Logo>() ?? new Logo();
    public static implicit operator string(Logo picture) => picture.ToJson();

}
