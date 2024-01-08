using Identity.Application.Common.Notifications.Services;
using Identity.Application.Common.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Identity.Infrastructure.Common.Notification.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly EmailSenderSettings _emailSenderSettings;

    public EmailOrchestrationService(IOptions<EmailSenderSettings> emailSenderSettings) =>
        _emailSenderSettings = emailSenderSettings.Value;

    public ValueTask<bool> SendMessageAsync(string emailAddress, string message)
    {
        var gmail = new MailMessage(_emailSenderSettings.CredentialAddress, emailAddress);
        gmail.Subject = "Siz muvofiqiyatli ro'yxatdan o'tdingiz";
        gmail.Body = message;

        var smtp = new SmtpClient(_emailSenderSettings.Host, _emailSenderSettings.Port);
        smtp.Credentials = new NetworkCredential(_emailSenderSettings.CredentialAddress, _emailSenderSettings.Password);
        smtp.EnableSsl = true;

        try
        {
            smtp.Send(gmail);
            return new(true);
        }
        catch(Exception ex)
        {
            return new(false);
        }
    }
}