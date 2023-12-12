using Moq;
using Renderer.Models.GmailWidget;
using Renderer.Services.GoogleServices;

namespace Renderer.Tests.GmailWidgetModelTests
{
    [TestClass]
    public class Set_Client_Id_And_Secret_Should
    {
        [TestMethod]
        public void SetTheClientIdAndSecretCorrectly()
        {
            // Arrange
            var googleAuthneticationMock = new Mock<IGoogleAuthentication>();
            var googleContactsMock = new Mock<IGoogleContacts>();
            var gmailPostman = new Mock<IGmailPostman>();

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);
            var clientId = "123";
            var clientSecret = "321";

            // Act
            sut.SetClientIdAndSecret(clientId, clientSecret);

            // Assert
            Assert.AreEqual(sut.ClientId, clientId);
            Assert.AreEqual(sut.ClientSecret, clientSecret);
        }
    }
}
