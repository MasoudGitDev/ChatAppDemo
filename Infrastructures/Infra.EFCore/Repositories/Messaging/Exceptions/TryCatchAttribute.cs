using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Exceptions;
public class TryCatchAttribute : Attribute, IAsyncActionFilter {
    public async Task OnActionExecutionAsync(ActionExecutingContext context , ActionExecutionDelegate next) {
        try {
            await next();
        }
        catch(DbUpdateException ex) {
            Console.WriteLine("Database Update Error At : " + context.ActionDescriptor.DisplayName + " : " + ex.Message.ToString());
        }
        catch(Exception ex) {           
            Console.WriteLine("Error At : " + context.ActionDescriptor.DisplayName + " : " + ex.Message.ToString());
        }
    }
}
