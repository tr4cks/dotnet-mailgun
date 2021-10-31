using Microsoft.Extensions.Options;

namespace Mailgun.Client.DependencyInjection; 

public class InjectableMailgunClient : MailgunClient {
    public InjectableMailgunClient(IOptions<MailgunClientOptions> optionsAccessor) :
        base(optionsAccessor.Value) {}
}
