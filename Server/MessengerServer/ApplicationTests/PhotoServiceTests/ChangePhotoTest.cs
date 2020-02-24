using Application.Models.PhotoDto;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using Domain.Exceptions.UserExceptions;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace ApplicationTests.PhotoServiceTests
{
    public class ChangePhotoTest
    {
        [Fact]
        public async void ChangePhoto_UserNotExist_ThrowsException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockAuth = fixture.Freeze<Mock<IAuthService>>();
            mockAuth.Setup(a => a.FindByNameUserAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            var photoService = fixture.Create<PhotoService>();

            //assert
            await Assert.ThrowsAsync<UserNotExistException>
                (async () => await photoService.ChangePhotoAsync(new AddPhotoDto()));
        }
    }
}
