using Microsoft.Extensions.Options;

namespace Mailgun.Client.DependencyInjection;

internal class InjectableMailgunClient : MailgunClient
{
    public InjectableMailgunClient(
        HttpClient client, IOptions<MailgunClientOptions> optionsAccessor) :
        base(client, optionsAccessor.Value)
    { }
}
