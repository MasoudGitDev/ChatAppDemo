using Apps.Auth.Accounts.Abstractions.Models;
using Apps.Auth.Accounts.Repos;
using Domains.Auth.UserEntity;
using Infra.EfCore.Auth.Exceptions;
using Infra.EfCore.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using Shared.Models;
using Shared.ValueObjects;

namespace Infra.EfCore.Auth.Repos;  
internal class AccountRepo(SignInManager<AppUser> signInManager , IJweService jweService) : IAccountRepo {
 
    // fields or props
    private readonly UserManager<AppUser> userManager = signInManager.UserManager ;

    // publics
    public Task LogoutAsync(string jweToken) {
       return Task.CompletedTask;
    }
    public async Task<AccountResult> RegisterAsync(IRegisterModel model , CancellationToken cancellationToken) {
        try {
            await CheckUserNotExistAsync(model.Email , model.UserName);
            EntityId entityId = new EntityId(Guid.NewGuid() , "AspNetUsers");
            var accountResult =  await jweService.GenerateTokenAsync(entityId);
            if(String.IsNullOrWhiteSpace(accountResult.JweToken)) {
                throw new AccountException("RegisterAsync" , "NullOrWhitespace" , "the <jweToken> can not be null or whitespace");
            }
            var createUser = new AppUser{Id = entityId,Email = model.Email, UserName = model.UserName};
            var creationResult = await userManager.CreateAsync(createUser);
            if(!creationResult.Succeeded) {
                var firstError = creationResult.Errors.First();
                throw new AccountException("RegisterAsync : On CreateAsync Method." , firstError.Code , firstError.Description);
            }
            var setPasswordResult = await userManager.AddPasswordAsync(createUser, model.Password);
            if(!setPasswordResult.Succeeded) {
                var firstError = setPasswordResult.Errors.First();
                throw new AccountException("RegisterAsync : On AddPasswordAsync Method." , firstError.Code , firstError.Description);
            }
            return accountResult;
        }    
        catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            return new AccountResult(string.Empty , new());
        }
    }

    public async Task<AccountResult> LoginByModelAsync(ILoginModel model) {
        AppUser? findUser = null;
        switch(model.LoginType) {
            case LoginTypes.ByEmail:
                findUser = await FindUserByEmailAsync(model.LoginName);
                break;

            case LoginTypes.ByUserName:
                findUser = await FindUserByUserNameAsync(model.LoginName);
                break;
        }
        return await TryToLoginAsync(findUser , model.Password , model.LoginType);
    }

    // privates
  
    private async Task<AppUser?> FindUserByEmailAsync(string email) => await userManager.FindByEmailAsync(email);
    private async Task<AppUser?> FindUserByUserNameAsync(string userName) => await userManager.FindByNameAsync(userName);
    private async Task CheckUserNotExistAsync(string email, string userName) {
        var findUser = await FindUserByEmailAsync(email);
        if(findUser != null) { throw new AccountException("CheckUserNotExist" , "ExistEmail" , "Your <email> must be unique.");}
        findUser = await FindUserByUserNameAsync(userName);
        if(findUser != null) { throw new AccountException("CheckUserNotExist" , "ExistUserName" , "Your <userName> must be unique."); }
    }      
    private async Task<AccountResult> TryToLoginAsync(AppUser? findUser , string password, LoginTypes loginType) {
        if(findUser != null && ( await signInManager.CheckPasswordSignInAsync(findUser , password , false) ).Succeeded) {            
            return await jweService.GenerateTokenAsync(findUser.Id);
        }
        string chooseName = LoginTypes.ByUserName == loginType ? "userName" : "email"; 
        throw new AccountException("TryToLoginAsync" , "UnknownUser" , $"<{chooseName}> or <password> is wrong.");
    }

    public async Task<AccountResult> LoginByTokenAsync(string jweToken) {
      return new AccountResult(jweToken , await jweService.GetClaimsByTokenAsync(jweToken));
    }
}