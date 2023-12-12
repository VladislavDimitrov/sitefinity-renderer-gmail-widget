using Microsoft.AspNetCore.Mvc;
using Renderer.Dto.Contacts;
using Renderer.Dto.SendGmail;
using Renderer.Models.GmailWidget;

namespace Renderer.Api.Controllers
{
    [Route("api/gmail-widget")]
    [ApiController]
    public class GmailWidgetApiController : ControllerBase
    {
        private readonly IGmailWidgetModel model;

        public GmailWidgetApiController(IGmailWidgetModel model)
        {
            this.model = model;
        }

        [HttpGet]
        [Route("authenticate")]
        public IActionResult AuthenticateUser()
        {
            try
            {
                this.model.AuthenticateUser();
            }
            catch (Exception)
            {
                //TODO: log exception, integrate with https://github.com/datalust/dotnet6-serilog-example
                return StatusCode(500, "Internal Server Error");
            }

            if (!this.model.UserIsAuthenticated())
                return this.BadRequest();

            return this.Ok();
        }

        [HttpGet]
        [Route("sign-out")]
        public IActionResult SignOutUser()
        {
            try
            {
                this.model.SignOutUser();
            }
            catch (Exception)
            {
                //TODO: log exception, integrate with https://github.com/datalust/dotnet6-serilog-example
                return StatusCode(500, "Internal Server Error");
            }

            if (this.model.UserIsAuthenticated())
                return this.BadRequest();

            return this.Ok();
        }

        [HttpGet]
        [Route("get-contacts")]
        public IActionResult GetContacts()
        {
            ContactDto[] contacts;
            try
            {
                contacts = this.model.GetContactsForCurrentUser() ?? new ContactDto[0];
            }
            catch (Exception)
            {
                //TODO: log exception, integrate with https://github.com/datalust/dotnet6-serilog-example
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(contacts);
        }

        [HttpPost]
        [Route("send-gmail")]
        public IActionResult SendGmail(SendGmailDto sendGmailDto)
        {
            if (sendGmailDto.Recipients?.Count() <= 0)
            {
                return this.BadRequest("At least 1 recipient is required.");
            }
            try
            {
                this.model.SendGmail(sendGmailDto);
            }
            catch (Exception)
            {
                //TODO: log exception, integrate with https://github.com/datalust/dotnet6-serilog-example
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }
    }
}


