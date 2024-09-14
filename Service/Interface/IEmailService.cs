using System.Net.Mail;
using Domain;

namespace Service.Interface;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage allMails, List<MemoryStream> memoryStreams);
}