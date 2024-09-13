namespace Domain.Options;

public class StripeSettings
{
    public const string Key = "StripeSettings";
    public string PublishableKey { get; set; }
    public string SecretKey { get; set; }
}