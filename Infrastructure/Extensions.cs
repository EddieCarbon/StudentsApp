using Microsoft.AspNetCore.Builder;
using NLog.Web;

namespace Infrastructure;

public static class Extensions
{
    public static ConfigureHostBuilder UseInfrastructure(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseNLog();
        
        return hostBuilder;
    }
}