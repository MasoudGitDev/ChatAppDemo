using System.Text;
using System.Text.Json;
namespace Shared.Extensions;  
public static class JsonExtensions {
    private static JsonSerializerOptions JsonSerializerOptions() => new() {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip
    };
    public static string? ToJson(this object? obj) => obj is null ? null : JsonSerializer.Serialize(obj , JsonSerializerOptions());
    public static T FromJsonTo<T>(this string? jsonSource) {
       if(jsonSource == null) {
            return default(T)!;
       }
       T? TObject =  JsonSerializer.Deserialize<T>(jsonSource , JsonSerializerOptions());
       if (TObject == null) {
            throw new ArgumentNullException($"Type {typeof(T)} is null.Can not convert the content of the string to json of type {typeof(T)}.");
       }
       return TObject;
    }
    public static StringContent ToStringContent<T>(this T instance) {
        if(instance is null) {
            throw new ArgumentNullException($"For converting {typeof(T)} to <StringContent> , {typeof(T)} can not be null.");
        }
       return new(instance.ToJson()! , Encoding.UTF8 , "application/json");
    }

}
