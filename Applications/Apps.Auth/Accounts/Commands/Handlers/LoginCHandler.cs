using Apps.Auth.Accounts.Commands.Models;
using Apps.Auth.Accounts.Repos;
using MediatR;
using Shared.Models;
namespace Apps.Auth.Accounts.Commands.Handlers;

internal record class LoginCHandler(IAccountRepo accountRepo) : IRequestHandler<LoginCModel, AccountResult>
{
    public async Task<AccountResult> Handle(LoginCModel request, CancellationToken cancellationToken)
    {
        return await accountRepo.LoginByModelAsync(request);
    }
}
