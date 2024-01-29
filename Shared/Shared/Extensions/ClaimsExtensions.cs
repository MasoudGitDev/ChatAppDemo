using System.Security.Claims;

namespace Shared.Extensions;
public static class ClaimsExtensions {
    public static List<Claim> ToClaims(this Dictionary<string , string> dicClaims) {
        var claims = new List<Claim>();
        foreach(var item in dicClaims) {
            claims.Add(new Claim(item.Key , item.Value));
        }
        return claims;
    }



}
