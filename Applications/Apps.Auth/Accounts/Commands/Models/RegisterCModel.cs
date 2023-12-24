using Apps.Auth.Accounts.Abstractions.Models;
using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;


namespace Apps.Auth.Accounts.Commands.Models;

public record RegisterCModel : IRegisterModel, IRequest<AccountResult>
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [Length(3, 50)]
    public string UserName { get; init; } = string.Empty;

    [Required]
    [Length(8, 50)]
    public string Password { get; init; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string PasswordConfirmed { get; init; } = string.Empty;
}
