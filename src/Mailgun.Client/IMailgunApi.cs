using Refit;

namespace Mailgun.Client;

internal partial interface IMailgunApi {
    [Post("/v3/{domain}/messages")]
    Task<HttpResponseMessage> SendMessage(string domain,
        [Authorize("Basic")] string credentials,
        [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> @params);
}

internal partial interface IMailgunApi {
    public static readonly (string Us, string Eu) BaseUrls = (
        "https://api.mailgun.net/", "https://api.eu.mailgun.net/");
    
    public static IMailgunApi CreateInstance(string? baseUrl = null) {
        baseUrl ??= BaseUrls.Us;
        return RestService.For<IMailgunApi>(baseUrl,
            new RefitSettings(new NewtonsoftJsonContentSerializer()));
    }

    public static IMailgunApi CreateInstance(HttpClient client) {
        return RestService.For<IMailgunApi>(client,
            new RefitSettings(new NewtonsoftJsonContentSerializer()));
    }
}
