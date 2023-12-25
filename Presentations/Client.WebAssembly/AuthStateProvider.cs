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
            var jweTokenFromLocalStorage = await localStorageService.GetItemAsStringAsync(_tokenKeyName);
            if(String.IsNullOrWhiteSpace(jweTokenFromLocalStorage)) {
                return new AuthenticationState(_anonymous);
            }
            return new AuthenticationState(await GetClaimsPrincipalAsync(jweTokenFromLocalStorage));
        }
        catch(Exception ex) {
            // Log the exception here
            Console.WriteLine(ex.ToString());
            return new AuthenticationState(_anonymous);
        }
    }
    public async Task SetAsync(AccountResult? accountResult) {
        try {
            ClaimsPrincipal claimsPrincipal = accountResult is null ? _anonymous : new(new ClaimsIdentity(accountResult.Claims.ToClaims()));
            if(accountResult is null) { await localStorageService.RemoveItemAsync(_tokenKeyName); }
            else { await localStorageService.SetItemAsStringAsync(_tokenKeyName , accountResult.JweToken); }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));         
        }
        catch (Exception ex) {
            Console.WriteLine("AuthStateProvider_SetAsync_err : " + ex.ToString());
        }
    }

    private async Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string jweToken) {
        var accountResult = await accountService.LoginByTokenAsync(new LoginByTokenDTO(jweToken));
        return new(accountResult.Claims is null ? new ClaimsIdentity() : new ClaimsIdentity(accountResult.Claims.ToClaims()));
    }
}
