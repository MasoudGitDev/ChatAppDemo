using Shared.Enums;

namespace Shared.Models;
public interface IResult {
    ResultStatus Status { get; }
    ErrorModel? Error { get; }
}
public record Result(ResultStatus Status , ErrorModel? Error) : IResult;
public record Result<T>(ResultStatus Status , ErrorModel? Error , T? Content) : IResult;
