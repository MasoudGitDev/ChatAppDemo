using Shared.Extensions;
using Shared.Models;

namespace Shared.Exceptions;  
public class CustomException :Exception{
    public string ClassName { get; } = "<UnDefined>";
    public string MethodName { get; } = "<Undefined>";
    public string Code { get; } = "<UnDefined>";
    public string Description { get; } = "<UnDefined>";

    public CustomException():base() {}
    public CustomException(string message) : base(message) { }

    //// must be delete later
    //public CustomException(string where , string code , string description) 
    //    : base($"{nL}{where}{nL}{code}{nL}{description}{nL}") {
    //    Code = code;   
    //    Description = description;
    //}

    public CustomException(string code,  string description) 
        : base($"{nL}Code: {code}{nL}Message: {description}{nL}") {
        Code = code;
        Description = description;
    }
   
    public CustomException(ExceptionModel model): base(message: model.ToJson()) {
        ClassName = model.ClassName;
        MethodName = model.MethodName;
        Code = model.Code;
        Description = model.Description;
    }
    private static string nL => Environment.NewLine ;
}
