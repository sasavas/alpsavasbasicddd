using BasicProjectTemplate.Application.UseCases.Users.Queries;
using MediatR;

namespace BasicProjectTemplate.Api.MiddleWares;

public class BlacklistedTokenCheckMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISender Sender;

    public BlacklistedTokenCheckMiddleware(RequestDelegate next, ISender sender)
    {
        _next = next;
        Sender = sender;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            
            if (token != null)
            {
                var isBlackListed = await Sender.Send(new BlackListedTokenQuery(token));
                if (isBlackListed)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is not blacklisted");
                    return;
                }
            }
        }

        await _next(context);
    }
}
