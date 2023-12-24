using Apps.Auth.Accounts.Commands.Models;
using Apps.Auth.Accounts.Repos;
using MediatR;
using Shared.Models;
namespace Apps.Auth.Accounts.Commands.Handlers;

internal record class RegisterCHandler(IAccountRepo accountRepo) : IRequestHandler<RegisterCModel, AccountResult>
{
    public async Task<AccountResult> Handle(RegisterCModel request, CancellationToken cancellationToken)
    {
        return await accountRepo.RegisterAsync(request, cancellationToken);
    }
}
