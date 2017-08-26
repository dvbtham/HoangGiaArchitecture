using System.Net.Mail;
using HoangGia.Utilities.Core;
using HoangGia.Service.Services;
using HoangGia.Model.Enums;

namespace HoangGia.Utilities.Helpers
{
    public static class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            var settingService = ServiceFactory.Get<ISettingService>();
            var setting = settingService.FindById((int) SettingValue.User);
            try
            {
                
                var host = setting.Smtp;
                var port = int.Parse(setting.SmtpPort);
                var fromEmail = setting.SmtpUsername;
                var password = setting.SmtpPassword;
                var fromName = setting.NotificationReplyEmail;

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);

                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }
    }
}