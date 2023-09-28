using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using System.Net;
using System.Net.Mail;

namespace PRIO.src.Shared.Utils.SendEmail
{
    public static class SendEmail
    {
        public static async Task Send(Client039DTO nfsm, User user)
        {
            using (var smtpClient = new SmtpClient("smtp.zoho.com")) //smtp
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("emailRemetente", "password");
                smtpClient.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress("emailRemetente");

                if (user.Email is not null)
                    message.To.Add(user.Email);
                else
                    message.To.Add(user.Username + "@prio3.com.br");

                message.Subject = $"Notificação de falha {nfsm.COD_FALHA_039}";
                message.Body = Template.GenerateNotificationEmail(nfsm);
                message.IsBodyHtml = true;
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
