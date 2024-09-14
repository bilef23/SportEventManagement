using System.Net.Mail;
using Domain;
using Domain.Options;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Service.Interface;

namespace Service.Implementation;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;

    public EmailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(EmailMessage allMails, List<MemoryStream> memoryStreams)
    {
        var emailMessage = new MimeMessage
        {
            Sender = new MailboxAddress("Sport Events Management", "bdimitrova35@yahoo.com"),
            Subject = allMails.Subject
        };

        emailMessage.From.Add(new MailboxAddress("Sport Events Management", "bdimitrova35@yahoo.com"));

        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = allMails.Content };

        emailMessage.To.Add(new MailboxAddress(allMails.MailTo, allMails.MailTo));
        var builder = new BodyBuilder
        {
            HtmlBody = "Thank you for your purchase!"
        };
        for (int i = 0; i < memoryStreams.Count; i++)
        {
            builder.Attachments.Add("Ticket-"+i+1+".pdf", memoryStreams[i], new ContentType("application", "pdf"));

        }
        emailMessage.Body = builder.ToMessageBody();

        // Add the PDF attachment
        try
        {
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                var socketOptions = SecureSocketOptions.Auto;

                await smtp.ConnectAsync(_mailSettings.SmtpServer, 587, socketOptions);

                if (!string.IsNullOrEmpty(_mailSettings.SmtpUserName))
                {
                    await smtp.AuthenticateAsync(_mailSettings.SmtpUserName, _mailSettings.SmtpPassword);
                }
                await smtp.SendAsync(emailMessage);


                await smtp.DisconnectAsync(true);
            }
        }
        catch (SmtpException ex)
        {
            throw ex;
        }
    }
}