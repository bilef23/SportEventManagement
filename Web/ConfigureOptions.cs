using Domain.Options;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Web;

public static class ConfigureOptions
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(configuration.GetSection(ConnectionStrings.Key));
        
    }
}