namespace Shared.Models;
public record AccountResult(string AuthToken , Dictionary<string,string> KeyValueClaims);
