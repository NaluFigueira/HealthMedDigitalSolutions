namespace PosTech.Hackathon.Appointments.Infra.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using PosTech.Hackathon.Appointments.Infra.Interfaces;

public class EmailService : IEmailService
{
    private readonly string _smtpServer = "smtp.yourmailserver.com"; // Replace with actual SMTP server
    private readonly string _smtpUser = "yourusername@domain.com";
    private readonly string _smtpPassword = "yourpassword";

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = 587,
            Credentials = new NetworkCredential(_smtpUser, _smtpPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("noreply@yourdomain.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(to);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
