using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> UserInfo(int UserId)
        {
            var userinfo = await _userservice.GetUserInfo(new GetUserInfoRequest() {UserId=UserId});

            if (userinfo != null)
                return Ok(userinfo);

            return BadRequest();
        }
    }
}