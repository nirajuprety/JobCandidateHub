using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using JobCandidateHub.Domain.Enum;

namespace JobCandidateHub.API.Services
{
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
