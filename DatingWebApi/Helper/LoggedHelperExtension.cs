using DatingWebApi.Repository;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatingWebApi.Helper
{
    public class LoggedHelperExtension : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionResult = next();
            var userId = int.Parse(actionResult.Result.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var repo = actionResult.Result.HttpContext.RequestServices.GetService<IDatingRepository>();
            var user = await repo.GetUser(userId);
            user.LastActive = DateTime.Now;

            await repo.SaveAll();
        }
    }
}
