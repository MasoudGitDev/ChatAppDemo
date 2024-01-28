using System.ComponentModel.DataAnnotations;

namespace Client.WebAssembly.Models.Accounts;
public record RegisterDTO {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Length(3 , 50)]
    public string UserName { get; set; }

    [Required]
    [Length(8 , 50)]
    public string Password { get; set; } 

    [Required]
    [Compare(nameof(Password))]
    public string PasswordConfirmed { get; set; }
}