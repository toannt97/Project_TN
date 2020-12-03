using Microsoft.Extensions.Configuration;
using ShopAPI.Constants;
using System;
using System.Net;
using System.Net.Mail;

namespace ShopAPI.Common
{
    public class Email
    {
        private readonly SmtpClient _clientDetails;
        private readonly MailMessage _mailDetails;
       
        public Email()
        {
            _clientDetails = new SmtpClient();
            _mailDetails = new MailMessage();
        }
        public void Send(string fromEmailID, string fromEmailPassword, string toEmail, string content, int port, string host, string subject)
        {
                // SMTP client details
                _clientDetails.Port = port;
                _clientDetails.Host = host;
                _clientDetails.EnableSsl = true;
                _clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                _clientDetails.UseDefaultCredentials = false;
                _clientDetails.Credentials = new NetworkCredential(fromEmailID.Trim(), fromEmailPassword.Trim());

                // Message details
                _mailDetails.From = new MailAddress(fromEmailID.Trim());
                _mailDetails.To.Add(toEmail.Trim());
                _mailDetails.Subject = subject;
                _mailDetails.IsBodyHtml = true;
                _mailDetails.Body = content;

                _clientDetails.Send(_mailDetails);
        }
    }


}
