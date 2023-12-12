using Moq;
using Renderer.Models.GmailWidget;
using Renderer.Services.GoogleServices;

namespace Renderer.Tests.GmailWidgetModelTests
{
    [TestClass]
    public class Is_Configured_Should
    {
        [TestMethod]
        public void ReturnTrueIfClientIdAndClientSecretHaveValues()
        {
            //Arrange
            var googleAuthneticationMock = new Mock<IGoogleAuthentication>();
            var googleContactsMock = new Mock<IGoogleContacts>();
            var gmailPostman = new Mock<IGmailPostman>();

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);

            //Act
            sut.SetClientIdAndSecret("12", "34");

            //Assert
            Assert.IsTrue(sut.IsConfigured());
        }

        [TestMethod]
        public void ReturnFalseIfClientIdAndClientSecretDontHaveValues()
        {
            //Arrange
            var googleAuthneticationMock = new Mock<IGoogleAuthentication>();
            var googleContactsMock = new Mock<IGoogleContacts>();
            var gmailPostman = new Mock<IGmailPostman>();

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);

            //Act, Assert
            Assert.IsTrue(sut.IsConfigured());
        }
    }
}
