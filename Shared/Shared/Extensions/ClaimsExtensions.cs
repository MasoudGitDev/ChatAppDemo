using Shared.Exceptions;
using System.Security.Claims;

namespace Shared.Extensions;  
public static class ClaimsExtensions {
    public static List<Claim> ToClaims(this Dictionary<string, string> dicClaims) {
        var claims = new List<Claim>();
        foreach(var item in dicClaims) {
            if(String.IsNullOrWhiteSpace(item.Key))
                throw new CustomException("ClaimsExtensions - ToClaims" , "NullOrWhiteSpace" , "item.key can not be NullOrWhiteSpace.");
            if(String.IsNullOrWhiteSpace(item.Value))
                throw new CustomException("ClaimsExtensions - ToClaims" , "NullOrWhiteSpace" , "item.value can not be NullOrWhiteSpace.");
            claims.Add(new Claim(item.Key , item.Value));
        }
        return claims;
    }


  
}
