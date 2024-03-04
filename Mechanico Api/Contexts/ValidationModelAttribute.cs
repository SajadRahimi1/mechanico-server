using Microsoft.AspNetCore.Mvc.Filters;

namespace Mechanico_Api.Contexts;

public class ValidationModelAttribute:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new ValidationFailedResult(context.ModelState);
        }
    }
}