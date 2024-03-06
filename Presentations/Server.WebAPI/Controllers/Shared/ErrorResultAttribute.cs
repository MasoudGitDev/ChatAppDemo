using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace Server.WebAPI.Controllers.Shared {
    public class ErrorResultAttribute : Attribute, IExceptionFilter {
        public void OnException(ExceptionContext context) {
            switch(context.Exception) {
                case CustomException ex:
                    context.Result = new JsonResult(new { ex.Code , ex.Message });
                    return;
                case Exception ex:
                    context.Result = new JsonResult(ex.Message);
                    return;
            }
        }
    }
}
