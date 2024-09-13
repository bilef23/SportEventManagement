using Domain.Options;
using Stripe;

namespace Web;

public static class AddServices
{
    public static void ConfigureWeb(this IServiceCollection services, IConfiguration configuration)
    {
        var stripeOptions = configuration.GetSection(StripeSettings.Key).Get<StripeSettings>();
        StripeConfiguration.ApiKey = stripeOptions.SecretKey;
    }
}