using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.UserDto;
using AutoMapper;
using Infrastructure.Services;
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

        private readonly AuthService _auth;

        private readonly IMapper _map;

        public UserController(IUserService userservice, AuthService auth, IMapper map)
        {
            _userservice = userservice;

            _auth = auth;

            _map = map;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _auth.FindByNameUserAsync(User.Identity.Name);

            if (user != null)
            {
                var dto = _map.Map<GetUserDto>(user);
                dto.Email = User.Identity.Name;
                return Ok(dto);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto model)
        {
            var res = await _userservice.UpdateUser(model);

            if (res)
                return Ok();

            return BadRequest();
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<List<SearchUserDto>> SearchUser([FromQuery]SearchUserDtoRequest request )
        {
           return await this._userservice.SearchUser(request);
        }
    }
}