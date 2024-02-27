using Shared.Models;

namespace Shared.Exceptions;
public class CustomException : Exception {
    public string ClassName { get; } = "<UnDefined>";
    public string MethodName { get; } = "<Undefined>";
    public string Code { get; } = "<UnDefined>";
    public string Description { get; } = "<UnDefined>";

    public CustomException() : base() { }
    public CustomException(string message) : base(message) { }

    public CustomException(string code , string description)
        : base(description) {
        Code = code;
        Description = description;
    }

    public CustomException(ExceptionModel model) : base(model.Description) {
        ClassName = model.ClassName;
        MethodName = model.MethodName;
        Code = model.Code;
        Description = model.Description;
    }
}
