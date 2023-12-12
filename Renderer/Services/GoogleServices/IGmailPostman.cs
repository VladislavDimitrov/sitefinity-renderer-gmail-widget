using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Renderer.Dto.SendGmail;
using static Renderer.Dto.SendGmail.SendGmailDto;

namespace Renderer.Services.GoogleServices
{
    public interface IGmailPostman
    {
        Message SendGmail(Message message, UserCredential credential);
        Message ComposeMessage(SendGmailDto dto, SenderInfo senderInfo);
    }
}