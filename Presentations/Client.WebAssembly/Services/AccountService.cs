using Client.WebAssembly.Exceptions;
using Client.WebAssembly.Models.Accounts;
using Shared.Extensions;
using Shared.Models;
using System.Net.Http.Headers;

namespace Client.WebAssembly.Services;
public interface IAccountService {
    Task<AccountResult> RegisterAsync(RegisterDTO model);
    Task<AccountResult> LoginAsync(LoginDTO model);
    Task<AccountResult> LoginByTokenAsync(LoginByTokenDTO model);
}
public class AccountService(HttpClient httpClient) : IAccountService {
    private const string _baseURL = "/api/Account";
    public async Task<AccountResult> RegisterAsync(RegisterDTO model)
        => await PostAsync(model , $"{_baseURL}/Register" , nameof(RegisterAsync));

    public async Task<AccountResult> LoginAsync(LoginDTO model)
        => await PostAsync(model , $"{_baseURL}/Login" , nameof(LoginAsync));

    public async Task<AccountResult> LoginByTokenAsync(LoginByTokenDTO model)
        => await PostAsync(model , $"{_baseURL}/LoginByToken" , nameof(LoginAsync));

    private async Task<AccountResult> PostAsync<T>(T model , string url , string methodName) {
        var response =  await httpClient.PostAsync(url, model.ToStringContent());
        if(response == null) {
            throw new AccountServiceException( "NullObj" , $"The <accountResult> object of {methodName} operation can not be null.");
        }
        if(!response.IsSuccessStatusCode) {
            throw new AccountServiceException(response.StatusCode.ToString() , response.Content.ToString() ?? "The Operation is not successful.");
        }
        var accountResult = (await response.Content.ReadAsStringAsync()).FromJsonTo<AccountResult>();
        if(accountResult is null) {
            throw new AccountServiceException("NullOrWhitespace" , "You Not Authenticated.");
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" , accountResult.AuthToken.Value);
        return accountResult;
    }

}
