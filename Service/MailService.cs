using System;
using System.Threading.Tasks;
using LHDTV.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
namespace LHDTV.Service
{
    public class MailService : IMailService
    {
        private readonly AppSettings settings;
        public MailService(IOptions<AppSettings> _appSettings ){
            settings = _appSettings.Value;
        }
        public async Task SendEmail(string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddres,
            string subject,
            string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(toName, toEmailAddres));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message
            };

            email.Body = body.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                try
                {
                    await client.ConnectAsync(settings.SmtpClient, settings.SmtpPort).ConfigureAwait(false);

                    await client.AuthenticateAsync(settings.EmailUser, settings.EmailPassw).ConfigureAwait(false);

                    await client.SendAsync(email).ConfigureAwait(false);

                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}