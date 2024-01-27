using Shared.Models;

namespace Shared.Exceptions;  
public class CustomException :Exception{
    public string Where { get; } = "UnDefined";
    public string Code { get; } = "UnDefined";

    public CustomException():base() {}
    public CustomException(string message) : base(message) { }
    public CustomException(string where , string code , string message) :
        base(message: $"{Environment.NewLine}Where : {where}{Environment.NewLine}Code : {code}{Environment.NewLine}Description : {message}{Environment.NewLine}") {
        Where = where;
        Code = code;
    }
    public CustomException(ExceptionModel model):
        base(message: $"{Environment.NewLine}Where : {model.Where}{Environment.NewLine}Code : {model.Code}{Environment.NewLine}Description : {model.Message}{Environment.NewLine}") {
        Where = model.Where;
        Code = model.Code;            
    }
    public override string ToString() {
        return $"Where : {Where} | Code : {Code} | Description : {Message}";
    }
}
