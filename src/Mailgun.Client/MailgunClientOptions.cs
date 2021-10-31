using System.ComponentModel.DataAnnotations;

namespace Mailgun.Client; 

public class MailgunClientOptions {
#nullable disable
    
    [Required]
    public string ApiKey { get; set; }

    [Required]
    public string Domain { get; set; }
    
#nullable restore
    
    public string? BaseUrl { get; set; }
}
