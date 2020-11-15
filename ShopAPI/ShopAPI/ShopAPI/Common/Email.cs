using System;
using System.Net;
using System.Net.Mail;

namespace ShopAPI.Common
{
    public class Email
    {
        private static readonly SmtpClient _clientDetails = new SmtpClient();
        private static readonly MailMessage _mailDetails = new MailMessage();


        public static bool Send(string fromEmail, string fromEmailPassword, string toEmail, string content, int port, string host, string subject)
        {
            try
            {
                // SMTP client details
                _clientDetails.Port = port;
                _clientDetails.Host = host;
                _clientDetails.EnableSsl = true;
                _clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                _clientDetails.UseDefaultCredentials = false;
                _clientDetails.Credentials = new NetworkCredential(fromEmail.Trim(), fromEmailPassword.Trim());

                // Message details
                _mailDetails.From = new MailAddress(fromEmail.Trim());
                _mailDetails.To.Add(toEmail.Trim());
                _mailDetails.Subject = subject;
                _mailDetails.IsBodyHtml = true;
                _mailDetails.Body = content;

                _clientDetails.Send(_mailDetails);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }


}
