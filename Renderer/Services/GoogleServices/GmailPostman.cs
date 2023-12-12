using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using MimeKit;
using Progress.Sitefinity.AspNetCore.Web.Security;
using Renderer.Dto.SendGmail;
using static Renderer.Dto.SendGmail.SendGmailDto;

namespace Renderer.Services.GoogleServices
{
    public class GmailPostman : IGmailPostman
    {
        public Message SendGmail(Message message, UserCredential credential) 
        {
            var gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GmailWidget",
            });
            var messageSent = gmailService.Users.Messages.Send(message, "me").Execute();

            return messageSent;
        }

        public Message ComposeMessage(SendGmailDto dto, SenderInfo senderInfo)
        {
            var sanitizer = new HtmlSanitizer();
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(senderInfo.Name, senderInfo.EmailAddress));

            foreach (var recipient in dto.Recipients)
            {
                msg.To.Add(new MailboxAddress(recipient.Name, recipient.EmailAddress));
            }

            if (!string.IsNullOrEmpty(dto.EmailSubject))
                msg.Subject = dto.EmailSubject;

            //compose body
            if (!string.IsNullOrEmpty(dto.EmailBody))
            {
                string sanitizedEmailBody = string.Empty;
                if (!string.IsNullOrEmpty(dto.EmailBody))
                    sanitizedEmailBody = sanitizer.Sanitize(dto.EmailBody);
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = sanitizedEmailBody;
                msg.Body = bodyBuilder.ToMessageBody();
            }

            //compose message
            var msgStream = new MemoryStream();
            msg.WriteTo(msgStream);
            var rawMessage = Convert.ToBase64String(msgStream.ToArray());
            var message = new Message { Raw = rawMessage };

            return message;
        }
    }
}
