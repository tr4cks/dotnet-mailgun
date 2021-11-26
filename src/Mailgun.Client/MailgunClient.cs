using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;

namespace Mailgun.Client;

public class MailgunClient : IMailgunClient {
    private readonly MailgunClientOptions _options;
    private readonly HttpClient _client;

    public MailgunClient(HttpClient client, MailgunClientOptions options) {
        var authorizationHeader =
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{options.ApiKey}"));
        client.BaseAddress = new Uri(options.BaseUrl ?? IMailgunClient.BaseUrls.Eu);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", authorizationHeader);

        _options = options;
        _client = client;
    }

    public Task<HttpResponseMessage> SendMessage(MailMessage message) {
        MultipartFormDataContent content = new() {
            { new StringContent(message.Subject), "subject" }
        };
        if (message.From is not null) {
            content.Add(
                new StringContent($"{message.From.DisplayName} <{message.From.Address}>"),
                "from");
        }
        foreach (var mailAddress in message.To) {
            content.Add(new StringContent(mailAddress.Address), "to");
        }
        content.Add(new StringContent(message.Body),
            message.IsBodyHtml ? "html" : "text");
        foreach (var attachment in message.Attachments) {
            if (attachment.Name is not null) {
                content.Add(new StreamContent(attachment.ContentStream), "attachment",
                    attachment.Name);
            }
            else {
                content.Add(new StreamContent(attachment.ContentStream), "attachment");
            }
        }
        return SendMessage(content);
    }

    private Task<HttpResponseMessage> SendMessage(HttpContent content) {
        HttpRequestMessage request = new(HttpMethod.Post,
            $"/v3/{_options.Domain}/messages") {
            Content = content
        };
        return _client.SendAsync(request);
    }
}
