using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace HealthMonitor
{
    public class EmailSender
    {
        public static void SendEmail(string body, string subject)
        {
            var from = ConfigurationManager.AppSettings["EmailSettingsFrom"];
            var recievers = ConfigurationManager.AppSettings["EmailSettingsTo"].Split(',');
            var password = ConfigurationManager.AppSettings["EmailSettingsPassword"];
            var displayName = ConfigurationManager.AppSettings["EmailSettingsDisplayName"];
            var fromAddress = new MailAddress(from, displayName);
            var server = ConfigurationManager.AppSettings["EmailSettingsSMTPServer"];
            var port = int.Parse(ConfigurationManager.AppSettings["EmailSettingsSMTPServerPort"]);

            foreach (var to in recievers)
            {
                var toAddress = new MailAddress(to);

                var smtp = new SmtpClient
                {
                    Host = server,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };
                try
                {
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    })
                    {
                        smtp.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    //Add Logging
                    throw ex;
                }
            }
        }
    }
}
