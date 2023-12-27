namespace Shared.Generics {
    public interface IValueObject {
        Guid Value { get; }
    }
    public interface IValueObject<T> where T:class , IValueObject , new() { }
}
