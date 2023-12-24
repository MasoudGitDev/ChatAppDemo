using Shared.Enums;

namespace Apps.Auth.Accounts.Abstractions.Models;
public interface ILoginModel {

    LoginTypes LoginType { get; set; }
    /// <summary>
    /// LoginName Can Be Email Or UserName.
    /// </summary>
    string LoginName { get; set; }
    string Password { get; set; }
}
