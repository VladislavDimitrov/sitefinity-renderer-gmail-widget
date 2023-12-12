using Google.Apis.Gmail.v1.Data;
using Google.Apis.PeopleService.v1.Data;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Dto.Contacts;
using Renderer.Dto.SendGmail;
using Renderer.Entities;

namespace Renderer.Models.GmailWidget
{
    public interface IGmailWidgetModel
    {
        void AuthenticateUser();

        bool UserIsAuthenticated();

        ContactDto[] GetContactsForCurrentUser();

        void SignOutUser();

        bool IsConfigured();

        Message SendGmail(SendGmailDto dto);

        void SetClientIdAndSecret(string clientIdValue, string clientSecretValue);
    }
}