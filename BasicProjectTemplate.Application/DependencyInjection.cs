using System.Reflection;
using BasicProjectTemplate.Application.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BasicProjectTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly());
            });

        services.AddSingleton<IUserIdProvider, UserIdProvider>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        return services;
    }
}