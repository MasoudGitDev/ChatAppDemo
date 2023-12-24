namespace Infra.EfCore.Auth.Exceptions;
internal class AccountException : Exception {
    public string Where { get; } = String.Empty;
    public string Code { get; } = String.Empty;
    public string Description { get; } = String.Empty;

    public AccountException(string where , string code , string description) :
        base(message : $"Where : {where}{Environment.NewLine}Code : {code}{Environment.NewLine}Description : {description}{Environment.NewLine}") {
        Where = where ;Code = code ;Description = description ;
    }

}
