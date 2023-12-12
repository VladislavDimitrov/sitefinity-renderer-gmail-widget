using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.PeopleService.v1.Data;
using Moq;
using Renderer.Models.GmailWidget;
using Renderer.Services.GoogleServices;

namespace Renderer.Tests.GmailWidgetModelTests
{
    [TestClass]
    public class Get_Contacts_For_Current_User_Should
    {
        [TestMethod]
        public void ReturnContactDtosWithMatchingEmails()
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

            var personsResult = new List<Person> {
                new Person {
            EmailAddresses = new List<EmailAddress> {
                new EmailAddress {
                    Value = "test@test.test" , Metadata = new FieldMetadata { Primary = true }
                }
            },
            Names = new List<Name> {
            new Name { DisplayName = "testName", Metadata = new FieldMetadata { Primary = true } } }
            } };

            googleContactsMock.Setup(m => m.GetCurrentUserContacts(userCredential)).Returns(personsResult);

            googleAuthneticationMock.Setup(g => g.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(userCredential);

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);

            //Act
            sut.AuthenticateUser();
            var contacts = sut.GetContactsForCurrentUser();

            //Assert
            Assert.AreEqual(contacts[0].RecipientEmaillAddress, personsResult[0].EmailAddresses[0].Value);

        }

        [TestMethod]
        public void ReturnContactDtosWithMatchingNames()
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

            var personsResult = new List<Person> {
                new Person {
            EmailAddresses = new List<EmailAddress> {
                new EmailAddress {
                    Value = "test@test.test" , Metadata = new FieldMetadata { Primary = true }
                }
            },
            Names = new List<Name> {
            new Name { DisplayName = "testName", Metadata = new FieldMetadata { Primary = true } } }
            } };

            googleContactsMock.Setup(m => m.GetCurrentUserContacts(userCredential)).Returns(personsResult);

            googleAuthneticationMock.Setup(g => g.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(userCredential);

            var sut = new GmailWidgetModel(googleAuthneticationMock.Object, googleContactsMock.Object, gmailPostman.Object);

            //Act
            sut.AuthenticateUser();
            var contacts = sut.GetContactsForCurrentUser();

            //Assert
            Assert.AreEqual(contacts[0].RecipientName, personsResult[0].Names[0].DisplayName);

        }
    }
}
