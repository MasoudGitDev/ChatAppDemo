using Client.WebAssembly.Models.Accounts;
using Client.WebAssembly.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Extensions;
using Shared.Models;
using System.Security.Claims;

namespace Client.WebAssembly;  
public class AuthStateProvider(ILocalStorageService localStorageService ,IAccountService accountService) 
    : AuthenticationStateProvider {

    private const string _tokenKeyName = "chatAuthToken";
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        try {
            var authTokenFromLocalStorage = await localStorageService.GetItemAsStringAsync(_tokenKeyName);
            if(String.IsNullOrWhiteSpace(authTokenFromLocalStorage)) {
                return new AuthenticationState(_anonymous);
            }
            return new AuthenticationState(await GetClaimsPrincipalAsync(authTokenFromLocalStorage));
        }
        catch(Exception ex) {
            // Log the exception here
            Console.WriteLine(ex.ToString());
            return new AuthenticationState(_anonymous);
        }
    }
    public async Task SetAuthenticationStateAsync(AccountResult? accountResult) {
        try {
            ClaimsPrincipal claimsPrincipal = accountResult is null ?
                _anonymous : new(new ClaimsIdentity(accountResult.KeyValueClaims.ToClaims()));
            if(accountResult is null) {
                await localStorageService.RemoveItemAsync(_tokenKeyName);
            }
            else { 
                await localStorageService.SetItemAsStringAsync(_tokenKeyName , accountResult.AuthToken.Value);
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));         
        }
        catch (Exception ex) {
            Console.WriteLine("AuthStateProvider_SetAsync_err : " + ex.ToString());
        }
    }

    private async Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string authToken) {
        var accountResult = await accountService.LoginByTokenAsync(new LoginByTokenDTO(authToken));
        return new(accountResult.KeyValueClaims is null ? new ClaimsIdentity() : new ClaimsIdentity(accountResult.KeyValueClaims.ToClaims()));
    }
}
