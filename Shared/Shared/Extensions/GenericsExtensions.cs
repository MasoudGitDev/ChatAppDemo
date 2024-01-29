using Shared.Exceptions;
using Shared.Models;

namespace Shared.Extensions;
public static class GenericsExtensions {
    public static T IfIsNull<T>(this T? source , ExceptionModel model) {
        if(source is null)
            throw new NullOrEmptyException(model);
        return source;
    }
    public static T IfIsNull<T>(this T? source) {
        if(source is null)
            throw new NullOrEmptyException("NullObj" , $"The {typeof(T).Name} can not be null.");
        return source;
    }

}
