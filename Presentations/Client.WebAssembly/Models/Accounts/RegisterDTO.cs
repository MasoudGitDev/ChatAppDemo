using System.ComponentModel.DataAnnotations;

namespace Client.WebAssembly.Models.Accounts;
public record RegisterDTO {
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Length(3 , 50)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [Length(8 , 50)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string PasswordConfirmed { get; set; } = string.Empty;
}