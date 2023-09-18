using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace MyAutoService.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("testtitoplearn@gmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("testtitoplearn@gmail.com", "TopE34@54Im");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return Task.CompletedTask;
        }
    }
}
