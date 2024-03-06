using Apps.Auth.Accounts.Abstractions.Models;
using Shared.Models;
using Shared.ValueObjects;

namespace Apps.Auth.Accounts.Repos;

public interface IAccountRepo
{
    /// <summary>
    ///  Note: The model validation must be done before passing model as an argument.  
    /// </summary>
    /// <exception cref="AccountException"></exception>
    Task<AccountResult> RegisterAsync(IRegisterModel model, CancellationToken cancellationToken);

    /// <summary>
    ///  Note: The model validation must be done before passing model as an argument.  
    /// </summary>
    /// <exception cref="AccountException"></exception>
    Task<AccountResult> LoginByModelAsync(ILoginModel model);


    /// <exception cref="AccountException"></exception>
    Task<AccountResult> LoginByTokenAsync(IAuthToken authToken);

    Task LogoutAsync(IAuthToken authToken);
}
