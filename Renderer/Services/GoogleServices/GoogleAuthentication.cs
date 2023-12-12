using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.PeopleService.v1;
using Google.Apis.Util;
using Google.Apis.Util.Store;

namespace Renderer.Services.GoogleServices.Authnetication
{
    public class GoogleAuthentication : IGoogleAuthentication
    {
        public UserCredential Authenticate(string clientId, string clientSecret)
        {
            var secrets = new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };
            var scopes = new string[] { GmailService.Scope.GmailSend, PeopleServiceService.Scope.ContactsReadonly };

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
              secrets,
              scopes,
                      "user",
                      CancellationToken.None,
                      new FileDataStore("store.json")).Result;

            if (credential.Token.IsExpired(SystemClock.Default))
            {
                GoogleWebAuthorizationBroker.ReauthorizeAsync(credential, CancellationToken.None);
            }

            return credential;
        }
    }
}
