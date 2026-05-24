using System;
using System.Configuration;
using System.Net.Mail;
using LegacyCRM.Core;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Web.Services
{
    public class EmailNotificationService
    {
        public void SendTicketCreatedNotification(Ticket ticket, Customer customer)
        {
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            string subject = "Ticket Created: " + ticket.Title;
            string body = "Customer: " + customer.CompanyName + Environment.NewLine + "Priority: " + ticket.Priority + Environment.NewLine + Environment.NewLine + ticket.Description;

            using (var context = new CrmDbContext())
            {
                var log = new EmailLog
                {
                    ToAddress = customer.Email,
                    Subject = subject,
                    Body = body,
                    SentAt = DateTime.UtcNow,
                    Status = "Sent"
                };

                try
                {
                    using (var client = new SmtpClient(smtpServer, smtpPort))
                    using (var message = new MailMessage(fromEmail, customer.Email, subject, body))
                    {
                        client.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    log.Status = "Failed";
                    log.ErrorMessage = ex.Message;
                }

                context.EmailLogs.Add(log);
                context.SaveChanges();
            }
        }
    }
}
