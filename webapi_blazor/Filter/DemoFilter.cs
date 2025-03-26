using Microsoft.AspNetCore.Mvc.Filters;
namespace webapi_blazor.Filter;
public class DemoFilter : ActionFilterAttribute
{
    public DemoFilter() {

    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // string? username = context.HttpContext.Request.Form["Username"];
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    
}