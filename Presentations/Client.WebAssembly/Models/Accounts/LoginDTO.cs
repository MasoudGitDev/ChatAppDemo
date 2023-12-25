namespace Client.WebAssembly.Models.Accounts;  
public record LoginDTO {
   // public LoginTypes LoginType { get; set; } = LoginTypes.ByUserName;
    public string LoginName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}
