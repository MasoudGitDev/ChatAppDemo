using Shared.Enums;

namespace Shared.Models;

public record Result(ResultStatus Status , ErrorModel? Error);
public record Result<T>(ResultStatus Status , ErrorModel? Error , T Content);
