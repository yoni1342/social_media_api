using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Galacticos.Application.Common.Interface.Services;
using Galacticos.Domain.Entities;

namespace Galacticos.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private const string SenderEmail = "nekahiwota@gmail.com";
        private const string SenderPassword = "qmbkhyqbhgvvfinl";
        private const string DisplayName = "Galacticos";


        public async Task SendEmail(Email email)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(SenderEmail, DisplayName);
            message.To.Add(email.To);
            message.Subject = email.Subject ?? DefaultMail.RegistrationSubject;
            message.Body = email.Body ?? DefaultMail.RegistrationBody;
            message.IsBodyHtml = true;

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                EnableSsl = true
            };
            
            await SendEmailAsync(client, message);
        }

        private static async Task SendEmailAsync(SmtpClient client, MailMessage message)
        {
            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}