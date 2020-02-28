using Application.Models.PhotoDto;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions.PhotoExceptions;
using Domain.Exceptions.UserExceptions;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApplicationTests.PhotoServiceTests
{
    public class GetPhotoTests
    {
        [Fact]
        public async void GetPhoto_UserNotExist_ThrowsException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockAuth = fixture.Freeze<Mock<IAuthService>>();
            mockAuth.Setup(a => a.FindByIdUserAsync(It.IsAny<int>()))
                .ReturnsAsync(default(User));

            var photoService = fixture.Create<PhotoService>();

            //assert
            await Assert.ThrowsAsync<UserNotExistException>
                (async () => await photoService.GetPhotoAsync(10));
        }

        [Fact]
        public async void GetPhoto_PhotoNotExist_ThrowsException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockAuth = fixture.Freeze<Mock<IUnitOfWork>>();
            mockAuth.Setup(a => a.ConversationInfoRepository.GetPhotoByUserAsync(It.IsAny<int>()))
                .ReturnsAsync(default(Photo));

            var photoService = fixture.Create<PhotoService>();

            //assert
            await Assert.ThrowsAsync<PhotoNotExistException>
                (async () => await photoService.GetPhotoAsync(10));
        }

        [Fact]
        public async void GetPhoto_InvokesOnce()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockUnit = fixture.Freeze<Mock<IUnitOfWork>>();

            var photoService = fixture.Create<PhotoService>();

            //act
            await photoService.GetPhotoAsync(10);


            //assert
            mockUnit.Verify(u => u.ConversationInfoRepository.GetPhotoByUserAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetPhoto_ReturnValidValues()
        {
            //arrange
            var photo = new Photo
            {
                Name = "testPhoto.png"
            };

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockUnit = fixture.Freeze<Mock<IUnitOfWork>>();
            mockUnit.Setup(u => u.ConversationInfoRepository.GetPhotoByUserAsync(It.IsAny<int>()))
                .ReturnsAsync(photo);

            var mockMap = fixture.Freeze<Mock<IMapper>>();
            mockMap.Setup(u => u.Map<GetPhotoDto>(It.Is<Photo>((p)=>p.Name==photo.Name)))
                .Returns(new GetPhotoDto {Name=photo.Name});

            var photoService = fixture.Create<PhotoService>();

            //act
            var result=await photoService.GetPhotoAsync(10);

            //assert
            Assert.True(photo.Name == result.Name);
        }
    }
}
