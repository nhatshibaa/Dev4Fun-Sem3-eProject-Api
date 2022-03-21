using Dev4Fun_Sem3_eProject.Models;
using Dev4Fun_Sem3_eProject.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dev4Fun_Sem3_eProject.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public bool SendConfirmEmail(MailConfirm mailConfirm)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(mailConfirm.UserName, mailConfirm.UserEmailId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = "Thông báo ứng tuyển thành công!";

                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\EmailConfirm.html";
                string EmailTemplateText = File.ReadAllText(FilePath);

                EmailTemplateText = string.Format(EmailTemplateText, mailConfirm.UserName, mailConfirm.Vacancy, mailConfirm.Time);

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = EmailTemplateText;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_mailSettings.Host, _mailSettings.Port);
                emailClient.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }

        public bool SendNoticeEmail(MailNotice mailNotice)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(mailNotice.UserName, mailNotice.UserEmailId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = "Thông báo lịch phỏng vấn!";

                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\EmailNotice.html";
                string EmailTemplateText = File.ReadAllText(FilePath);

                EmailTemplateText = string.Format(EmailTemplateText, mailNotice.Vacancy, mailNotice.UserName, mailNotice.Time, mailNotice.Who, mailNotice.InterviewLocation);

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = EmailTemplateText;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_mailSettings.Host, _mailSettings.Port);
                emailClient.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }

        public bool SendRefuseEmail(MailRefuse mailRefuse)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(mailRefuse.UserName, mailRefuse.UserEmailId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = "Thông báo từ chối!";

                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplate\\EmailRefuse.html";
                string EmailTemplateText = File.ReadAllText(FilePath);

                EmailTemplateText = string.Format(EmailTemplateText, mailRefuse.UserName, mailRefuse.Vacancy);

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = EmailTemplateText;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_mailSettings.Host, _mailSettings.Port);
                emailClient.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}
