using Domains.Messaging.Shared.Exceptions;
using Shared.Extensions;

namespace Domains.Messaging.Shared.ValueObjects;
public record Logo {
    public byte Id { get; }
    public string Image { get; } = string.Empty;
    public string? Title { get; set; }

    private Logo() { }
    private Logo(string image , string? title , byte id) {
        if(String.IsNullOrWhiteSpace(image)) {
            throw new LogoException("NullOrWhiteSpace" , "The <image> can not be NullOrWhiteSpace.");
        }

        Image = image;
        Title = title;
        Id = id;
    }

    public static Logo Create(string image , string? title , byte id)
        => new(image , title , id);

    public static implicit operator Logo(string? jsonSource) => jsonSource.FromJsonTo<Logo>();
    public static implicit operator string?(Logo logo) => logo.ToJson();

}
