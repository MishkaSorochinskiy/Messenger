using Application.Models.PhotoDto;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Domain.Exceptions.UserExceptions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //[Fact]
        //public async void ChangePhoto_GetPhoto_InvokesOnce()
        //{
        //    //arrange
        //    var fixture = new Fixture().Customize(new AutoMoqCustomization());

        //    var mockUnit = fixture.Freeze<Mock<IUnitOfWork>>();

        //    var mockFile = new Mock<IFormFile>();
        //    mockFile.Setup(f => f.FileName)
        //        .Returns("Test.Extension");

        //    var mockConfigSection = fixture.Freeze<Mock<IConfigurationSection>>();
        //    mockConfigSection.Setup(confSec => confSec.Get(typeof(string[])))
        //        .Returns(new string[] { });

        //    var mockConfig= fixture.Freeze<Mock<IConfiguration>>();
        //    mockConfig.Setup(c => c.GetSection(It.IsAny<string>()))
        //        .Returns(mockConfigSection.Object);

        //    var photoService = fixture.Create<PhotoService>();

        //    //act
        //    await photoService.ChangePhotoAsync(new AddPhotoDto() 
        //    {
        //        UploadedFile=mockFile.Object
        //    });

        //    //assert
        //    mockUnit.Verify(u => u.PhotoRepository.GetPhotoByUserAsync(It.IsAny<int>()), Times.Once);
        //}
    }
}
