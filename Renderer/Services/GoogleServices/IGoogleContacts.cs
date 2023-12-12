using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1.Data;

namespace Renderer.Services.GoogleServices
{
    public interface IGoogleContacts
    {
        List<Person> GetCurrentUserContacts(UserCredential credential);
    }
}