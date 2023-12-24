﻿using Apps.Auth.Accounts.Commands.Models;
using Apps.Auth.Accounts.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Auth.Accounts.Commands.Handlers;

internal class LoginByTokenCHandler(IAccountRepo accountRepo) : IRequestHandler<LoginByTokenCModel, AccountResult>
{
    public async Task<AccountResult> Handle(LoginByTokenCModel request, CancellationToken cancellationToken)
    {
        return await accountRepo.LoginByTokenAsync(request.JweToken);
    }
}
