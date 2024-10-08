using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    class NotificationService
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.mailserver.com"); // Use your SMTP server

                mail.From = new MailAddress("your-email@example.com");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;

                smtpClient.Port = 587; // or your SMTP port
                smtpClient.Credentials = new NetworkCredential("your-email@example.com", "your-password");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
