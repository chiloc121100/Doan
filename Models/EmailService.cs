using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("locdcgcs200461@fpt.edu.vn", _configuration["EmailSettings:FromEmail"]));
        message.To.Add(new MailboxAddress(toEmail, toEmail)); // Corrected usage
        message.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = htmlMessage;

        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, false);
        await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
