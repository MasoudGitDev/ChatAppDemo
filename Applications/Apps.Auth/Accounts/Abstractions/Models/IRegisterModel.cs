namespace Apps.Auth.Accounts.Abstractions.Models;
public interface IRegisterModel
{
    string Email { get; init; }
    string UserName { get; init; }
    string Password { get; init; }
    string PasswordConfirmed { get; init; }
}
