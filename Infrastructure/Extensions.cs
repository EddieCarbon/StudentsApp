using Core.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        
        services.AddDbContext<StudentAppContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("StudentCS")));
        
        return services;
    }
    public static ConfigureHostBuilder UseInfrastructure(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseNLog();
        
        return hostBuilder;
    }
}