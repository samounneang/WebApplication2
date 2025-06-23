using AgriAuth.DTO;
using System.Net.Mail;
using System.Net;

namespace AgriAuth.Repository.EmailRepository
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailDto dto)
        {
            var smtpSetting = _configuration.GetSection("SmtpSettings");

            string host = smtpSetting["Host"] ?? throw new Exception("SMTP Host is missing.");
            string portString = smtpSetting["Port"] ?? throw new Exception("SMTP Port is missing.");
            string username = smtpSetting["Username"] ?? throw new Exception("SMTP Username is missing.");
            string password = smtpSetting["Password"] ?? throw new Exception("SMTP Password is missing.");
            string fromEmail = smtpSetting["FromEmail"] ?? throw new Exception("SMTP FromEmail is missing.");
            string enableSslString = smtpSetting["EnableSsl"] ?? "true"; // Default to true

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(portString) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(enableSslString))
            {
                throw new Exception("SMTP settings are missing in configuration.");
            }

            int port = int.Parse(portString);
            bool enableSsl = bool.Parse(enableSslString);

            var smtpClient = new SmtpClient(host)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(dto.ToEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }


    }

}


