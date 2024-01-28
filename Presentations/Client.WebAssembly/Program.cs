using Blazored.LocalStorage;
using Client.WebAssembly;
using Client.WebAssembly.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
//Default BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7071") });
builder.Services.AddScoped<IAccountService , AccountService>();
builder.Services.AddScoped<AuthenticationStateProvider , AuthStateProvider>();
builder.Services.AddScoped<IGroupService , GroupService>();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
