using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasicProjectTemplate.Application.DataAccess;

public static class DependencyInjection
{
    public static IServiceProvider ServiceProvider = null!;
    
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnectionString");

        services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention()
                    .EnableSensitiveDataLogging();
            });
        
        if (environment.IsDevelopment())
        {
        }
        else
        {
        }

        ServiceProvider = services.BuildServiceProvider();
        
        return services;
    }
}