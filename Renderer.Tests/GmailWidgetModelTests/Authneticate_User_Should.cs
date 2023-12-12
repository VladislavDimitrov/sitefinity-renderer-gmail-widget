using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Moq;
using Renderer.Models.GmailWidget;
using Renderer.Services.GoogleServices;

namespace Renderer.Tests.GmailWidgetModelTests
{
    [TestClass]
    public class Authneticate_User_Should
    {
        [TestMethod]
        public void SetTheCredential()
        {
            // Arrange
            var googleAuthneticationMock = new Mock<IGoogleAuthentication>();
            var googleContactsMock = new Mock<IGoogleContacts>();
            var gmailPostman = new Mock<IGmailPostman>();

            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "your-client-id",
                    ClientSecret = "your-client-secret"
                }
            };

            var userCredential = new UserCredential(new GoogleAuthorizationCodeFlow(initializer), "user", new TokenResponse());

            googleAuthneticationMock.Setup(g => g.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(userCredential);

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);

            // Act, Assert
            Assert.IsNull(sut.Credential);
            sut.AuthenticateUser();
            Assert.IsNotNull(sut.Credential);
        }
    }
}
