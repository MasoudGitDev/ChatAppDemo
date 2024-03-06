using Shared.Enums;

namespace Shared.Models;
public interface IResult {
    ResultStatus Status { get; }
    ResultMessage? ResultMessage { get; }
}
public record ResultMessage(string Code , string Message) {
    public ResultMessage(string Where , string Code , string Message):this(Code,Message)
    {
        
    }
};

public record Result(ResultStatus Status , ResultMessage? ResultMessage) : IResult;
public record Result<T>(ResultStatus Status , ResultMessage? ResultMessage , T? Content) : IResult;

