using Apps.Auth.Accounts.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(ISender sender) : ControllerBase {

    [HttpPost("Login")]
    public async Task<AccountResult> LoginAsync([FromBody]LoginCModel loginModel) {
        return await sender.Send(loginModel);
    }

    [HttpPost("LoginByToken")]
    public async Task<AccountResult> LoginByTokenAsync([FromBody]LoginByTokenCModel loginByTokenModel) {
        return await sender.Send(loginByTokenModel);
    }

    [HttpPost("Register")]
    public async Task<AccountResult> RegisterAsync([FromBody]RegisterCModel registerModel) {
        return await sender.Send(registerModel);
    }

}
