using Infra.EFCore.Contexts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EFCore.Exceptions;

internal sealed class ConcurrencyTryCatchAttribute<T> : Attribute, IAsyncActionFilter where T : CustomException
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string where = $"{context.Controller.GetType().Name} : {context.ActionDescriptor.DisplayName}";
        try
        {
            await next();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            AppDbContext appDbContext = context.HttpContext.RequestServices.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            var entry = ex.Entries.Single();
            await entry.ReloadAsync();
            await appDbContext.SaveChangesAsync();
            throw (T) Activator.CreateInstance(typeof(T) , where , ex.GetType().Name , ex.Message)!;
        }
        catch (Exception ex)
        {
            throw (T)Activator.CreateInstance(typeof(T), where, ex.GetType().Name, ex.Message)!;
        }
    }
}