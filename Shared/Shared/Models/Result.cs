namespace Shared.Models;

public record Result(bool Success , List<ErrorModel>? Reports);
public record Result<T>(bool Success , List<ErrorModel>? Reports , T Content);
public record ErrorModel(string Where,string Code , string Message);