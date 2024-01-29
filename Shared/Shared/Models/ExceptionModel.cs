namespace Shared.Models;
public interface IExceptionModel {
    public string Code { get; init; }
    public string Description { get; init; }
    public string ClassName { get; init; }
    public string MethodName { get; init; }
}
public record ExceptionModel1(string Where , string Code , string Description) : IExceptionModel {
    public string ClassName { get; init; }
    public string MethodName { get; init; }
};
public record ExceptionModel(
    string ClassName ,
    string MethodName ,
    string Code ,
    string Description) : IExceptionModel;
