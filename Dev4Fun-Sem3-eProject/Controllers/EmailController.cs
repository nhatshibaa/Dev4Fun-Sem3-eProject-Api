using Dev4Fun_Sem3_eProject.Models;
using Dev4Fun_Sem3_eProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

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

