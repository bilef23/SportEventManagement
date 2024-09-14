using Domain.Options;

namespace Web;

public static class ConfigureOptions
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(configuration.GetSection(ConnectionStrings.Key));
        services.Configure<StripeSettings>(configuration.GetSection(StripeSettings.Key));
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.Key));
    }
}