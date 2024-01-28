namespace Client.WebAssembly.Extensions;

public static class ObjectExtensions {
    public static T IfNotNull<T>(this T? instance) {
        if(instance == null)
            throw new ArgumentNullException($"{typeof(T)} can not be null.");
        return instance;
    }

    public static string MustNotNullOrWhiteSpace(this string value , string whereHappened) {
        if(string.IsNullOrEmpty(value)) {
            throw new ArgumentNullException($"{whereHappened} : The value must be not null or white space.");
        }
        return value;
    }
}
