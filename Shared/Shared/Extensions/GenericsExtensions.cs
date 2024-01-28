using Shared.Exceptions;
using Shared.Models;

namespace Shared.Extensions;
public static class GenericsExtensions {
    public static T CheckNullability<T>(this T? source , ExceptionModel model) {
        if(source is null)
            throw new NullOrEmptyException(model);
        return source;
    }
    public static T CheckNullability<T>(this T? source , string where) {
        if(source is null)
            throw new NullOrEmptyException(where , "NullObj" , $"The {typeof(T).Name} can not be null.");
        return source;
    }


}
