using Google.Apis.Auth.OAuth2;

namespace Renderer.Services.GoogleServices
{
    public interface IGoogleAuthentication
    {
        UserCredential Authenticate(string clientId, string clientSecret);
    }
}