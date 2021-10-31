namespace Mailgun.Client; 

public interface IMailgunClient {
    Task<HttpResponseMessage> SendMessage(string from, string to,
        string subject, string text);
}
