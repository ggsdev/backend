using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using System.Net;
using System.Net.Mail;

namespace PRIO.src.Shared.Utils.SendEmail
{
    public static class SendEmail
    {
        public static async Task Send(Client039DTO nfsm, User user)
        {
            using (var smtpClient = new SmtpClient("smtp.zoho.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("gabriel.garcia@dbit.srv.br", "!Likeabr2");
                smtpClient.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress("gabriel.garcia@dbit.srv.br");

                if (user.Email is not null)
                    message.To.Add(user.Email);
                else
                    message.To.Add(user.Username + "@prio3.com.br");

                message.Subject = "Test Email";
                message.Body = Template.GenerateNotificationEmail(nfsm);
                message.IsBodyHtml = true;
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
