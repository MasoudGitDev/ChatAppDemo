using Apps.Auth.Accounts.Abstractions.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;
using System.ComponentModel.DataAnnotations;


namespace Apps.Auth.Accounts.Commands.Models;

public record LoginCModel : ILoginModel ,IRequest<AccountResult>
{
    [Required]
    [Length(3, 50)]
    public string LoginName { get; set; } = string.Empty;

    [Required]
    [Length(8, 50)]
    public string Password { get; set; } = string.Empty;
    public LoginTypes LoginType { get; set; } = LoginTypes.ByUserName;
}
