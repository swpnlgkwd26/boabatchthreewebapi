using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_webapi.Filters
{
    public class CustomActionFilter :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var parameter = context.ActionArguments.SingleOrDefault(); // Parameter Action has Received
            if (parameter.Value == null)
            {
                context.Result = new BadRequestObjectResult("Model is Null");                 
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Model State is Invalid");
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }

}


