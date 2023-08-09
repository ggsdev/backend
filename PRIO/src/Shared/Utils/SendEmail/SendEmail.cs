using PRIO.src.Modules.FileImport.XML.Dtos;
using System.Net;
using System.Net.Mail;

namespace PRIO.src.Shared.Utils.SendEmail
{
    public static class SendEmail
    {
        public static void Send(Client039DTO nfsm)
        {
            using (var smtpClient = new SmtpClient("smtp.zoho.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("gabriel.garcia@dbit.srv.br", "!Likeabr2");
                smtpClient.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress("gabriel.garcia@dbit.srv.br");
                message.To.Add("garciasholding@gmail.com");
                message.Subject = "Test Email";
                message.Body = Template.GenerateNotificationEmail(nfsm);
                message.IsBodyHtml = true;
                smtpClient.Send(message);
            }
        }
    }
}
