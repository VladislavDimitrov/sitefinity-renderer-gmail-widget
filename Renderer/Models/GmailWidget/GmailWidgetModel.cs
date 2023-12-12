using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.PeopleService.v1.Data;
using Renderer.Dto.Contacts;
using Renderer.Dto.SendGmail;
using Renderer.Services.GoogleServices;

namespace Renderer.Models.GmailWidget
{
    public class GmailWidgetModel : IGmailWidgetModel
    {
        private static string clientId;
        private static string clientSecret;
        private static UserCredential credential;
        private static ContactDto[] contacts;

        private readonly IGoogleAuthentication authneticatorService;
        private readonly IGoogleContacts contactsService;
        private readonly IGmailPostman gmailService;

        public GmailWidgetModel(IGoogleAuthentication authneticatorService, IGoogleContacts contactsService, IGmailPostman gmailService)
        {
            this.authneticatorService = authneticatorService;
            this.contactsService = contactsService;
            this.gmailService = gmailService;
        }

        public string ClientId => clientId;
        public string ClientSecret => clientSecret;
        public UserCredential Credential => credential;
        public ContactDto[] Contacts => contacts;

        public void SetClientIdAndSecret(string clientIdValue, string clientSecretValue)
        {
            clientId = clientIdValue;
            clientSecret = clientSecretValue;
        }

        public void AuthenticateUser()
        {
            credential = authneticatorService.Authenticate(clientId, clientSecret);
        }

        public void SignOutUser()
        {
            if (credential != null)
            {
                credential.RevokeTokenAsync(CancellationToken.None);
                credential = null;
                contacts = null;
            }
        }

        public ContactDto[] GetContactsForCurrentUser()
        {
            if (contacts is null && credential != null)
            {
                var userContacts = contactsService.GetCurrentUserContacts(credential);
                contacts = this.NormalizeContacts(userContacts);
            }

            return contacts;
        }

        public bool UserIsAuthenticated()
        {
            return credential is null ? false : true;
        }

        public bool IsConfigured()
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                return false;

            return true;
        }

        public Message SendGmail(SendGmailDto dto)
        {
            var composedMessage = gmailService.ComposeMessage(dto, new SendGmailDto.SenderInfo
            {
                Name = "me",
                EmailAddress = credential.UserId
            });
            var sentMessage = gmailService.SendGmail(composedMessage, credential);

            return sentMessage;
        }

        private ContactDto[] NormalizeContacts(List<Person> userContacts)
        {
            var normalizedContacts = userContacts
                                    .Where(c => c.EmailAddresses != null && c.EmailAddresses.Any())
                                    .Select(c => new ContactDto
                                    {
                                        RecipientName = c.Names.FirstOrDefault(n => n.Metadata.Primary.Value)?.DisplayName ?? c.Names.FirstOrDefault()?.DisplayName,
                                        RecipientEmaillAddress = c.EmailAddresses.FirstOrDefault(ea => ea.Metadata.Primary.Value)?.Value ?? c.EmailAddresses.FirstOrDefault()?.Value
                                    })
                                    .ToArray();

            return normalizedContacts;
        }
    }
}
