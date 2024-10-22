using JobCandidateHub.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace JobCandidateHub.API.Services
{
    [ExcludeFromCodeCoverage]
    public class InputValidationFilter : IActionFilter, IFilterMetadata
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> list = (from e in context.ModelState.Values.SelectMany((ModelStateEntry v) => v.Errors)
                                     select e.ErrorMessage into e
                                     orderby e
                                     select e).ToList();
                JsonResult result = new JsonResult(new
                {
                    Data = 0,
                    Message = list[0],
                    Status = StatusType.ProcessError,
                })
                {
                    StatusCode = 400
                };
                context.Result = result;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

}
