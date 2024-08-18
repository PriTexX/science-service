using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Science.DB;

namespace Science.Middlewares;

public sealed class AdminAuthFilter : AuthorizeAttribute, IAsyncActionFilter
{
    public AdminAuthFilter() { }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var appCtx = context.HttpContext.RequestServices.GetService<ApplicationContext>();

        ArgumentNullException.ThrowIfNull(appCtx);

        var userGuid = context.HttpContext.User.FindFirst(c => c.Type == "IndividualGuid")?.Value;

        if (userGuid is null)
        {
            context.Result = new ObjectResult(new { error = "Unauthorized" }) { StatusCode = 401 };
            return;
        }

        var adminUser = await appCtx.AdminUsers.FindAsync(userGuid);

        if (adminUser is null)
        {
            context.Result = new ObjectResult(new { error = "NotAllowed" }) { StatusCode = 403 };
            return;
        }

        await next();
    }
}
