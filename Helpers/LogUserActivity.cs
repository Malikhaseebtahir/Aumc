using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Aumc.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Aumc.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = Convert.ToInt32(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var repository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var unitOfWork = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await repository.GetUserAsync(userId, true);
            
            user.LastActive = DateTime.Now;

            await unitOfWork.CompleteAsync();
        }
    }
}