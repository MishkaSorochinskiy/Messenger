﻿using Application.Models.UserDto.Requests;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain;
using Domain.Entities;
using Domain.Exceptions.BlockedUserExceptions;
using Domain.Exceptions.UserExceptions;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApplicationTests.UserServiceTests
{
    public class UnBlockUserTest
    {
        [Fact]
        public async void UnBlockUser_RequestedUserNotExist_ThrowsException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            
            var mock = fixture.Freeze<Mock<IAuthService>>();
            mock.Setup(a => a.FindByIdUserAsync(It.IsAny<int>()))
                    .ReturnsAsync(default(User));

            var userService = fixture.Create<UserService>();

            //assert
            await Assert.ThrowsAsync<UserNotExistException>(async () => await userService.UnBlockUserAsync(new BlockUserRequest()));
        }

        [Fact]
        public async void UnBlockUser_UserToBeUnBlockedIsAlreadyUnBlocked_ThrowsException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockUnit = fixture.Freeze<Mock<IUnitOfWork>>();
            mockUnit.Setup(u => u.BlockedUserRepository.IsBlockedUserAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(default(BlockedUser));
                
            var userService = fixture.Create<UserService>();

            //assert
            await Assert.ThrowsAsync<BlockedUserNotExistException>(async () => await userService.UnBlockUserAsync(new BlockUserRequest()));
        }

        [Fact]
        public async void UnBlockUser_RemoveBlock_InvokesOnce()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockUnit = fixture.Freeze<Mock<IUnitOfWork>>();
            mockUnit.Setup(u => u.BlockedUserRepository.IsBlockedUserAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new BlockedUser());

            var userService = fixture.Create<UserService>();

            //act
            await userService.UnBlockUserAsync(new BlockUserRequest());

            //assert
            mockUnit.Verify(u => u.BlockedUserRepository.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
