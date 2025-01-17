﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.ChatDto.Requests;
using Application.Models.UserDto;
using Application.Models.UserDto.Requests;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IAuthService _auth;

        private readonly IMapper _map;

        public UserController(IUserService userService, IAuthService auth, IMapper map)
        {
            _userService = userService;

            _auth = auth;

            _map = map;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            var userInfo=await this._userService.GetUserInfoAsync(new GetUserInfoRequest() 
            { 
                UserName = User.Identity.Name 
            });

            return Ok(userInfo);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto model)
        {
            model.UserId = (int)HttpContext.Items["id"];

            await _userService.UpdateUserAsync(model);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<List<SearchUserDto>> Search([FromQuery]SearchUserDtoRequest request )
        {
           request.UserId = (int)HttpContext.Items["id"];

            return await this._userService.SearchUserAsync(request);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BlockUser([FromBody]BlockUserRequest request)
        {
            request.UserId = (int)HttpContext.Items["id"]; ;

            await this._userService.BlockUserAsync(request);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UnBlockUser([FromBody]BlockUserRequest request)
        {
            request.UserId = (int)HttpContext.Items["id"];

            await this._userService.UnBlockUserAsync(request);

            return Ok();
        }
    }
}