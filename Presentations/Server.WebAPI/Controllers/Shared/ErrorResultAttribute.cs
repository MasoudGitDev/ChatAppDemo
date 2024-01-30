using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shared.Exceptions;
using Shared.Models;

namespace Server.WebAPI.Controllers.Shared {
    public class ErrorResultAttribute : Attribute, IAsyncExceptionFilter {
           
        public async Task OnExceptionAsync(ExceptionContext context) {          
            switch(context.Exception) {
                case CustomException ex:
                    context.Result = await CreateErrorResult(new ExceptionModel(ex.ClassName , ex.MethodName ,
                        ex.Code , ex.Message));
                    return;
                case Exception ex:
                    context.Result = await CreateErrorResult(ex.Message);
                    return;
            }
        }

        private async Task<IActionResult> CreateErrorResult(ExceptionModel model)
            => await Task.FromResult(new ViewResult() {
                ViewName = "ErrorResult" ,
                ContentType = "application/json" ,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider() , new ModelStateDictionary()) {
                    Model =  new CustomException(model)
                }
            });
        private async Task<IActionResult> CreateErrorResult(string message)
           => await Task.FromResult(new ViewResult() {
               ViewName = "ErrorResult" ,
               ContentType = "application/json" ,
               ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider() , new ModelStateDictionary()) {
                   Model = new CustomException(message)
               }
           });
    }
}
