using System.Text;

namespace Mailgun.Client; 

public class MailgunClient : IMailgunClient {
    private readonly MailgunClientOptions _options;
    private readonly string _authorizationHeader;
    private readonly IMailgunApi _client;

    public MailgunClient(MailgunClientOptions options) {
        _options = options;
        _authorizationHeader =
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{options.ApiKey}"));
        _client = IMailgunApi.CreateInstance(options.BaseUrl);
    }

    public Task<HttpResponseMessage> SendMessage(string from, string to,
        string subject, string text) {
        Dictionary<string, object> @params = new() {
            {"from", from},
            {"to", to},
            {"subject", subject},
            {"text", text}
        };
        return _client.SendMessage(_options.Domain, _authorizationHeader, @params);
    }
}
