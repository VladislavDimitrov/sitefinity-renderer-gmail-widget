using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Renderer.Dto.Contacts;

namespace Renderer.Services.GoogleServices
{
    public class GoogleContacts : IGoogleContacts
    {

        public List<Person> GetCurrentUserContacts(UserCredential credential)
        {
            var peopleService = new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GmailWidget",
            });

            ContactDto[] normalizedContacts = new ContactDto[0];
            var connections = peopleService.People.Connections.List("people/me");
            connections.RequestMaskIncludeField = "person.names,person.emailAddresses";
            var googleContacts = connections.Execute()?.Connections;

            return googleContacts?.ToList();
        }
    }
}
