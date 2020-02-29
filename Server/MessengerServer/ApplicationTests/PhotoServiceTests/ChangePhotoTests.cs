﻿//using Application.Models.PhotoDto;
//using AutoFixture;
//using AutoFixture.AutoMoq;
//using Domain.Entities;
//using Domain.Exceptions.UserExceptions;
//using Infrastructure.Services;
//using Xunit;
//using Moq;
//using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Http;

//namespace ApplicationTests.PhotoServiceTests
//{
//    public class ChangePhotoTests
//    {
//        [Fact]
//        public async void ChangePhoto_UserNotExist_ThrowsException()
//        {
//            //arrange
//            var fixture = new Fixture().Customize(new AutoMoqCustomization());

//            var mockAuth = fixture.Freeze<Mock<IAuthService>>();
//            mockAuth.Setup(a => a.FindByIdUserAsync(It.IsAny<int>()))
//                .ReturnsAsync(default(User));

//            var photoService = fixture.Create<PhotoService>();

//            //assert
//            await Assert.ThrowsAsync<UserNotExistException>
//                (async () => await photoService.ChangePhotoAsync(new AddPhotoDto()));
//        }

//        [Fact]
//        public async void ChangePhoto_ExtensionNotExist_ThrowsException()
//        {
//            //arrange
//            var fixture = new Fixture().Customize(new AutoMoqCustomization());

//            var mockConfig = fixture.Freeze<Mock<IConfiguration>>();
//            mockConfig.SetupGet(c => c[It.IsAny<string>()])
//                .Returns(default(string));

//            var fileMock = new Mock<IFormFile>();
//            fileMock.SetupGet(file => file.FileName)
//                .Returns("photo.extension");

//            var request = fixture.Build<AddPhotoDto>()
//                .With(p => p.UploadedFile,fileMock.Object)
//                .Create();

//            var photoService = fixture.Create<PhotoService>();

//            //assert
//            await Assert.ThrowsAsync<PhotoInCorrectException>(async () => await photoService.ChangePhotoAsync(request));
//        }
//    }
//}
