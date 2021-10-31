using Microsoft.Extensions.Options;

namespace Mailgun.Client.DependencyInjection; 

internal class InjectableMailgunClient : MailgunClient {
    public InjectableMailgunClient(IOptions<MailgunClientOptions> optionsAccessor) :
        base(optionsAccessor.Value) {}
}
