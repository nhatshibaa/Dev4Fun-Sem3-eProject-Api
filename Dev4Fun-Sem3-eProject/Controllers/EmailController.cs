using Dev4Fun_Sem3_eProject.Data;
using Dev4Fun_Sem3_eProject.Models;
using Dev4Fun_Sem3_eProject.Services;
using Dev4Fun_Sem3_eProject.Settings;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dev4Fun_Sem3_eProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [Route("SendConfirmEmail")]
        [HttpPost]
        public bool SendConfirmEmail([FromForm] MailConfirm mailConfirm)
        {
            return mailService.SendConfirmEmail(mailConfirm);
        }

        [Route("SendNoticeEmail")]
        [HttpPost]
        public bool SendNoticeEmail([FromForm] MailNotice mailNotice)
        {
            return mailService.SendNoticeEmail(mailNotice);
        }

        [Route("SendRefuseEmail")]
        [HttpPost]
        public bool SendRefuseEmail([FromForm] MailRefuse mailRefuse)
        {
            return mailService.SendRefuseEmail(mailRefuse);
        }
    }
}

