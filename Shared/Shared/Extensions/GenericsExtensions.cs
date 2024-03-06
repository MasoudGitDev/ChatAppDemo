using Shared.Exceptions;
using Shared.Models;
using Shared.ValueObjects;

namespace Shared.Extensions;
public static class GenericsExtensions {
    public static T ThrowIfNull<T>(this T? source , ExceptionModel model) {
        if(source is null)
            throw new NullOrEmptyException(model);
        return source;
    }
    public static Dictionary<string , string> ThrowIfNullOrEmpty(this Dictionary<string , string>? source , string message) {
        if(source is null)
            throw new NullOrEmptyException("NullObj" , message);
        if(!source.Any())
            throw new NullOrEmptyException("Empty" , message);
        return source;
    }
    public static T ThrowIfNull<T>(this T? source , string? message = null) {
        if(source is null)
            throw new NullOrEmptyException("NullObj" , message ?? $"The {typeof(T).Name} can not be null.");
        return source;
    }
    public static string ThrowIfNullOrWhiteSpace(
        this string? source, 
        string name = "string" ,
        string? message = null) {
        if(String.IsNullOrWhiteSpace(source)) {            
            throw new NullOrEmptyException("NullOrWhiteSpace" , message ?? $"The <{name}> can not be <NullOrWhiteSpace>");
        }         
        
        return source;
    }

    public static T? ThrowIfNotNull<T>(this T? source , string code = "NotNull" , string? message = null) {
        if(source is not null)
            throw new NotNullException(code , message ?? $"The {typeof(T).Name} must be null.");
        return source;
    }

    public static T? ThrowIfFound<T>(this T? source , string? message = null) {
        if(source is not null)
            throw new NotNullException("Found" , message ?? $"The {typeof(T).Name} must be null or not found.");
        return source;
    }

    

}
