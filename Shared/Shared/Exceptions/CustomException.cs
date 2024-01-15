﻿namespace Shared.Exceptions;  
public class CustomException :Exception{
    public string Where { get; } = String.Empty;
    public string Code { get; } = String.Empty;
    public string Description { get; } = String.Empty;

    public CustomException(string where , string code , string description) :
        base(message: $"{Environment.NewLine}Where : {where}{Environment.NewLine}Code : {code}{Environment.NewLine}Description : {description}{Environment.NewLine}") {
        Where = where;
        Code = code;
        Description = description;
    }
    public override string ToString() {
        return $"Where : {Where} | Code : {Code} | Description : {Description}";
    }
}
