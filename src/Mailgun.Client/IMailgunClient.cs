using System.Net.Mail;

namespace Mailgun.Client;

public interface IMailgunClient
{
    static readonly (string Us, string Eu) BaseUrls = (
        "https://api.mailgun.net/", "https://api.eu.mailgun.net/");

    Task<HttpResponseMessage> SendMessage(MailMessage message);
}
