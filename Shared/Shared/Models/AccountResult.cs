using Shared.ValueObjects;

namespace Shared.Models;
public record AccountResult(IAuthToken AuthToken , Dictionary<string,string> KeyValueClaims);
