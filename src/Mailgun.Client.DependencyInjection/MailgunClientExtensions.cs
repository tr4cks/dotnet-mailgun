using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mailgun.Client.DependencyInjection;

public static class MailgunClientExtensions {
    static void PrepareMailgunClient(this IServiceCollection @this) {
        @this.AddHttpClient<IMailgunClient, InjectableMailgunClient>();
    }

    public static IServiceCollection AddMailgunClient(this IServiceCollection @this) {
        @this.AddMailgunClient(setupAction: null);
        return @this;
    }

    public static IServiceCollection AddMailgunClient(
        this IServiceCollection @this,
        Action<MailgunClientOptions>? setupAction) {
        @this.PrepareMailgunClient();
        if (setupAction is not null) {
            @this.AddOptions<MailgunClientOptions>()
                .Configure(setupAction)
                .ValidateDataAnnotations();
        }
        return @this;
    }

    public static IServiceCollection AddMailgunClient(
        this IServiceCollection @this,
        IConfiguration configuration) {
        @this.PrepareMailgunClient();
        @this.AddOptions<MailgunClientOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        return @this;
    }
}
