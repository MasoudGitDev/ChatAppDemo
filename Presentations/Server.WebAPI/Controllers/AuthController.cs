using Domains.Messaging.Shared.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.WebAPI.Controllers;

[ApiController]
[Authorize]
public class AuthController : ControllerBase {

    protected ClaimsPrincipal GetUser() {
        if(User.Identity is not null && User.Identity.IsAuthenticated) {
            return User;
        }
        return new ClaimsPrincipal();
    }

    protected AppUserId GetUserId() {
        return (GetUser().Claims.Where(x => x.Type == "UserIdentifier").FirstOrDefault())?.Value ?? String.Empty;
    }
}
