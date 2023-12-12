using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Moq;
using Renderer.Models.GmailWidget;
using Renderer.Services.GoogleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer.Tests.GmailWidgetModelTests
{
    [TestClass]
    public class User_Is_Authenticated_Should
    {
        [TestMethod]
        public void ShouldReturnFalseIfCredentialIsNull()
        {
            //Arrange 
            var googleAuthneticationMock = new Mock<IGoogleAuthentication>();
            var googleContactsMock = new Mock<IGoogleContacts>();
            var gmailPostman = new Mock<IGmailPostman>();

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);

            //Act, Assert
            Assert.IsNull(sut.Credential);
            Assert.IsFalse(sut.UserIsAuthenticated());
        }

        [TestMethod]
        public void ShouldReturnTrueIfCredentialIsNotNull()
        {
            //Arrange 
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
            sut.AuthenticateUser();
            Assert.IsNotNull(sut.Credential);
            Assert.IsTrue(sut.UserIsAuthenticated());
        }
    }
}
