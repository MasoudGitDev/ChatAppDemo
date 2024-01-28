using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shared.Exceptions;

namespace Server.WebAPI.Controllers.Shared {
    public class ErrorResultAttribute : Attribute, IAsyncExceptionFilter {       

        public async Task OnExceptionAsync(ExceptionContext context) {
            string where = context.ExceptionDispatchInfo?.SourceException.GetType().Name ?? "Unknown";
            switch(context.Exception) {
                case CustomException ex:
                    context.Result = await CreateErrorResult(ex.Where , ex.Code , ex.Message);
                    return;
                case Exception ex:
                    context.Result = await CreateErrorResult(where , ex.GetType().Name , ex.Message);
                    return;
            }
        }

        private async Task<IActionResult> CreateErrorResult(string where , string exceptionName , string message)
            => await Task.FromResult(new ViewResult() {
                ViewName = "ErrorResult" ,
                ContentType = "application/json" ,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider() , new ModelStateDictionary()) {
                    Model =  new CustomException(where , exceptionName ,message)
                }
            });
    }
}
