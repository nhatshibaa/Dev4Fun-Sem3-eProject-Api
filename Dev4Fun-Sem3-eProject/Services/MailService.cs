using Dev4Fun_Sem3_eProject.Data;
using Dev4Fun_Sem3_eProject.Models;
using Dev4Fun_Sem3_eProject.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dev4Fun_Sem3_eProject.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly Dev4Fun_Sem3_eProjectContext _context;


        public MailService(IOptions<MailSettings> options, Dev4Fun_Sem3_eProjectContext context)
        {
            _mailSettings = options.Value;
            _context = context;
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

                var vacancies = new Vacancies();
                var applicants = new Applicants();

                vacancies = _context.Vacancies.FromSqlRaw(@"select * from Vacancies where Id = {0}", mailConfirm.IdVacancy).First();
                applicants = _context.Applicants.FromSqlRaw(@"select * from Applicants where Id = {0}", mailConfirm.IdApplicant).First();

                EmailTemplateText = string.Format(EmailTemplateText, applicants.ApplicantName, vacancies.Title, mailConfirm.Time);

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

                var vacancies = new Vacancies();
                var applicants = new Applicants();
                var accounts = new Accounts();

                //vacancies = _context.Vacancies.Find(mailNotice.IdVacancy);
                vacancies = _context.Vacancies.FromSqlRaw(@"select * from Vacancies where Id = {0}", mailNotice.IdVacancy).First();
                applicants = _context.Applicants.FromSqlRaw(@"select * from Applicants where Id = {0}", mailNotice.IdApplicant).First();
                //applicants = _context.Applicants.Find(mailNotice.IdUser);

                EmailTemplateText = string.Format(EmailTemplateText, vacancies.Title, applicants.ApplicantName, mailNotice.Time.AddDays(3), mailNotice.IdMentor, mailNotice.InterviewLocation, mailNotice.IdHr);

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

                var vacancies = new Vacancies();
                var applicants = new Applicants();

                vacancies = _context.Vacancies.FromSqlRaw(@"select * from Vacancies where Id = {0}", mailRefuse.IdVacancy).First();
                applicants = _context.Applicants.FromSqlRaw(@"select * from Applicants where Id = {0}", mailRefuse.IdApplicant).First();


                EmailTemplateText = string.Format(EmailTemplateText, applicants.ApplicantName, vacancies.Title);

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


