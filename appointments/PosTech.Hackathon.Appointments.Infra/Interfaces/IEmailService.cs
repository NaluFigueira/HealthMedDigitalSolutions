namespace PosTech.Hackathon.Appointments.Infra.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

