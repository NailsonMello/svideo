using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SVideo.API.Models.Responses;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SVideo.API.Configurations.Filters
{
    /// <summary>
    /// Validate Model State Filter Attribute class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValidateModelStateFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// On Action Executing
        /// </summary>
        /// <param name="context">Action Executing Context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors;

                errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                BussinessExceptionResponse responseObj = new BussinessExceptionResponse(errors);

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
