using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;

namespace MVCCore03Osama.Email
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            var fromAddress = new MailAddress("ايميلك", "From Name");
            var toAddress = new MailAddress(userEmail, "To Name");
            const string fromPassword = "باسووردك";
            const string subject = "Subject";
            string body = confirmationLink;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return true;
            /*******************************************/
            /*MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("hodamosaad0@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("hodamosaad0@gmail.com", "allahakbar19971996hodaibrahim");
            client.Host = "smtp.gmail.com";
            client.Port = 25;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }*/
            return false;
        }
    }
}
