using MediatR;
using Shared.Models;

namespace Apps.Auth.Accounts.Commands.Models;
public record LoginByTokenCModel(string JweToken) : IRequest<AccountResult>;
